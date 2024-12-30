using ParkingService.Data;
using ParkingService.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkingService.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ParkingSpotContext _context;

        public ReservationRepository(ParkingSpotContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Reservation>> GetAllAsync()
        {
            return await _context.reservations
                .Include(r => r.user_id)
                .Include(r => r.parking_spot_id)
                .ToListAsync();
        }

        public async Task<Reservation?> GetByIdAsync(int id)
        {
            return await _context.reservations
                .Include(r => r.user_id)
                .Include(r => r.parking_spot_id)
                .FirstOrDefaultAsync(r => r.id == id);
        }

        public async Task<Reservation> CreateAsync(Reservation reservation)
        {
            await _context.reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
            return reservation;
        }

        public async Task<Reservation?> UpdateAsync(Reservation reservation)
        {
            var existingReservation = await _context.reservations.FindAsync(reservation.id);
            if (existingReservation == null) return null;

            existingReservation.user_id = reservation.user_id;
            existingReservation.parking_spot_id = reservation.parking_spot_id;
            existingReservation.start_time = reservation.start_time;
            existingReservation.end_time = reservation.end_time;

            _context.reservations.Update(existingReservation);
            await _context.SaveChangesAsync();
            return existingReservation;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var reservation = await _context.reservations.FindAsync(id);
            if (reservation == null) return false;

            _context.reservations.Remove(reservation);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IEnumerable<Reservation>> GetReservationsByUserIdAsync(int userId)
        {
            return await _context.reservations
                .Include(r => r.parking_spot_id)
                .Where(r => r.user_id == userId)
                .ToListAsync();
        }
    }
}
