using GISWeb.Models;
using Npgsql;
using System.Data.Common;
using System.Runtime.Intrinsics.Arm;

namespace GISWeb.Services
{
    public class FeatureService : IFeatureService
    {
        public IConfiguration configuration { get; set; }

        private string? connectionString { get; set; }

        public FeatureService(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("GisDbContext");
        }

        public List<string> GetSpatialJoinData(int firstLayer, int spatialOperator, decimal? distance, int secondLayer)
        {
            var result = new List<string>();
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var leftLayer = (Layer)firstLayer;
            var rightLayer = (Layer)secondLayer;
            var stOperator = ((PostGISOperator)spatialOperator).ToString();
            var distanceWithin = spatialOperator == (int)PostGISOperator.ST_DWithin ? (", " + distance).ToString() : string.Empty;

            var defaultWhereFilter = GetDefaultFilterBySelectedLayers(leftLayer, rightLayer);
            var leftTable = GetFeatureTypeByLayer(leftLayer);
            var rightTable = GetFeatureTypeByLayer(rightLayer);

            var sqlSelect = string.Format(
                            @"SELECT json_build_object(
                                'type', 'FeatureCollection',
                                'features', json_agg(ST_AsGeoJSON(t.*)::json)
                            )
                            FROM (SELECT l.osm_id, l.amenity, l.area, l.boundary, l.building, l.construction, 
	                              l.highway, l.historic, l.landuse, l.layer, l.leisure, l.motorcar, l.name, l.""natural"",
	                              l.oneway, l.operator, l.place, l.population, l.railway, l.shop, l.sport, l.surface,
	                              l.toll, l.water, l.waterway, l.wood, ST_Transform(l.way,4326) way 
	                              FROM {0} as l
	                              JOIN {1} as r 
	                              ON {2}(l.way, r.way {3})
	                              WHERE ({4})
                            ) as t;",
                            leftTable, rightTable, stOperator, distanceWithin, defaultWhereFilter); 

            using (NpgsqlCommand command = new NpgsqlCommand(sqlSelect, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader.GetValue(0).ToString());
                }

                connection.Close();
            }

            return result;
        }

        public List<VehicleModel> GetAllVehicles()
        {
            var result = new List<VehicleModel>();
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var sqlSelect = @"SELECT DISTINCT vehicle_id, vehicle_type FROM sumo_fcd_data;";

            using (NpgsqlCommand command = new NpgsqlCommand(sqlSelect, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    var vehicle = new VehicleModel();
                    vehicle.Vehicle_Id = reader.GetValue(0).ToString();
                    vehicle.Vehicle_Type = reader.GetValue(1).ToString();

                    result.Add(vehicle);
                }

                connection.Close();
            }

            return result;
        }

        public List<string> GetTemporalData(int type, int temporalOperator, decimal value, int startTime, int endTime)
        {
            var result = new List<string>();
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            var havingQuery = string.Empty;
            var orderBy = string.Empty;
            if (type == (int)QueryType.NumberOfCar)
            {
                havingQuery = "COUNT(car.vehicle_id)";
                orderBy = "count_of_vehicle DESC";
            }
            else
            {
                havingQuery = "AVG(car.vehicle_speed)";
                orderBy = "average_speed DESC";
                value = value * 0.621371192M;
            }

            havingQuery += GetOperator(temporalOperator);
            havingQuery += value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            var sqlSelect = string.Format(
                            @"SELECT json_build_object(
                                'type', 'FeatureCollection',
                                'features', json_agg(ST_AsGeoJSON(t.*)::json)
                            )
                            FROM (
                                WITH C AS (
						            SELECT --car.vehicle_id, 
						            COUNT(car.vehicle_id) count_of_vehicle,
						            AVG(car.vehicle_speed) average_speed, road.name roadName
						            FROM sumo_fcd_data AS car 
						            JOIN planet_osm_line AS road
						            ON ST_DWithin(road.way, car.way, 4) AND road.name is not null
						            WHERE car.timestep_time >= {0} AND timestep_time <= {1}
						            GROUP BY car.vehicle_id, road.name
						            HAVING {2}
                                    LIMIT 100
	                            ) 
                                SELECT l.amenity, l.area, l.boundary, l.building, l.construction, 
	                                   l.highway, l.historic, l.landuse, l.layer, l.leisure, 
                                       l.motorcar, l.name, l.natural, l.oneway, l.operator, 
                                       l.place, l.population, l.railway, l.shop, l.sport, l.surface,
	                                   l.toll, l.water, l.waterway, l.wood, C.count_of_vehicle, 
                                       (C.average_speed * 1.609344) as average_speed, 
                                       ST_Transform(l.way,4326) way 
								FROM C
	                            JOIN  
                                planet_osm_line l 
                                ON C.roadName = l.name
                                LIMIT 100
                                ) as t;",
                                startTime, endTime, havingQuery, orderBy);

            using (NpgsqlCommand command = new NpgsqlCommand(sqlSelect, connection))
            {
                NpgsqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    result.Add(reader.GetValue(0).ToString());
                }

                connection.Close();
            }

            return result;
        }

        private string GetDefaultFilterBySelectedLayers(Layer leftLayer, Layer rightLayer)
        {
            var sqlWhere = GetDefaultFilterByLayer(leftLayer, "l");
            sqlWhere += " AND " + GetDefaultFilterByLayer(rightLayer, "r");

            return sqlWhere;
        }

        private string GetFeatureTypeByLayer(Layer layer)
        {
            switch (layer)
            {
                case Layer.Educational_institution:
                case Layer.Hospitals:
                case Layer.Sport_institutions:
                case Layer.Forests: 
                    return "planet_osm_polygon";
                case Layer.Gas_stations:
                case Layer.Pharmacies: 
                    return "planet_osm_point";
                case Layer.Railways:
                case Layer.Rivers:
                case Layer.Roads:
                    return "planet_osm_line";
                default:
                    return string.Empty;
            }
        }

        private string GetDefaultFilterByLayer(Layer layer, string prefix)
        {
            switch(layer)
            {
                case Layer.Educational_institution:
                    return "(" + prefix + ".amenity IN ('kindergarten', 'school', 'college', 'university','language_school') " +
                        "OR " + prefix + ".office='educational_institution')";
                case Layer.Hospitals:
                    return prefix + ".amenity IN ('hospital')";
                case Layer.Sport_institutions:
                    return prefix +".leisure IN ('pitch','sports_centre','sports_hall','stadium')";
                case Layer.Gas_stations:
                    return prefix + ".amenity IN ('fuel')";
                case Layer.Pharmacies:
                    return prefix + ".amenity IN ('pharmacy')";
                case Layer.Forests:
                    return "(" + prefix +".landuse IN ('forest') OR " + prefix + ".natural IN ('wood'))";
                case Layer.Railways:
                    return prefix + ".railway IS NOT NULL";
                case Layer.Rivers:
                    return prefix + ".waterway IN ('river', 'stream')";
                case Layer.Roads:
                    return prefix + ".highway IS NOT NULL";
                default:
                    return string.Empty;
            }
        }

        private string GetOperator(int value)
        {
            switch (value)
            {
                case 0: return "=";
                case 1: return "<";
                case 2: return ">";
                case 3: return ">=";
                case 4: return "<=";
                default: 
                    return string.Empty;
            }
        }
    }
}
