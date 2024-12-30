using ParkingService.Models;

namespace ParkingService.Services
{
    public interface IParkingSpotService
    {
        Task<IEnumerable<ParkingSpot>> GetAllParkingSpotsAsync();
        Task<string> GetParkingDataAsGeoJson(string shapefilePath);
        Task<ParkingSpot?> GetParkingSpotByIdAsync(int id);
        Task<ParkingSpot> AddParkingSpotAsync(ParkingSpot spot);
        Task<ParkingSpot?> UpdateParkingSpotAsync(int id, ParkingSpot updatedSpot);
        Task<bool> DeleteParkingSpotAsync(int id);
        string TransformToWGS84(string geoJson);
    }
}
