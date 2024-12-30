using ParkingService.Models;
using ParkingService.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ParkingService.Services
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _reservationRepository.GetAllAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _reservationRepository.GetByIdAsync(id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            return await _reservationRepository.CreateAsync(reservation);
        }

        public async Task<Reservation?> UpdateAsync(Reservation reservation)
        {
            return await _reservationRepository.UpdateAsync(reservation);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _reservationRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _reservationRepository.GetReservationsByUserIdAsync(userId);
        }
    }
}
