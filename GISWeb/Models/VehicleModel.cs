namespace GISWeb.Models
{
    public class VehicleViewModel
    {
        public List<VehicleModel> Vehicles { get; set; } = new List<VehicleModel>();
    }

    public class VehicleModel
    {
        public string? Vehicle_Id { get; set; }

        public string? Vehicle_Type { get; set; }
    }

    public enum VehicleType
    {
        Bike,
        Bus,
        Motorcycle,
        Vehicle
    }

    public enum PostGISVehicleType
    {
        bike_bicycle,
        bus_bus,
        motorcycle_motorcycle,
        veh_passenger
    }
}
