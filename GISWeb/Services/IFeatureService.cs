using GISWeb.Models;

namespace GISWeb.Services
{
    public interface IFeatureService
    {
        List<string> GetSpatialJoinData(int firstLayer, int spatialOperator, decimal? distance, int secondLayer);

        List<VehicleModel> GetAllVehicles();

        List<string> GetTemporalData(int type, int temporalOperator, decimal value, int startTime, int endTime);
    }
}
