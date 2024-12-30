using ParkingService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ParkingService.Repositories
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAllAsync();
        Task<Reservation?> GetByIdAsync(int id);
        Task<Reservation> CreateAsync(Reservation reservation);
        Task<Reservation?> UpdateAsync(Reservation reservation);
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId);
    }
}
