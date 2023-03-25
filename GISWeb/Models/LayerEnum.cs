namespace GISWeb.Models
{
    public enum Layer
    {
        Educational_institution,
        Hospitals,
        Sport_institutions,
        Gas_stations,
        Pharmacies,
        Forests,
        Rivers,
        Railways,
        Roads
    }

    public enum SpatialOperator
    {
        Intersects,
        Crosses,
        Contains,
        Within,
        Within_distance
    }

    public enum PostGISOperator
    {
        ST_Intersects,
        ST_Crosses,
        ST_Contains,
        ST_Within,
        ST_DWithin
    }

    public enum QueryType
    {
        NumberOfCar,
        AverageSpeed
    }
}
