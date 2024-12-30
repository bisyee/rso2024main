using ParkingService.Models;

namespace ParkingService.Repositories
{
       public interface IParkingSpotRepository
    {
        Task<IEnumerable<ParkingSpot>> GetAllAsync();
        Task<ParkingSpot?> GetByIdAsync(int id);
        Task<ParkingSpot> AddAsync(ParkingSpot spot);
        Task<ParkingSpot?> UpdateAsync(ParkingSpot spot);
        Task<bool> DeleteAsync(int id);
    }

}