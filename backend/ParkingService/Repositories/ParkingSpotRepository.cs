using Microsoft.EntityFrameworkCore;
using ParkingService.Data;
using ParkingService.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ParkingService.Repositories
{

    public class ParkingSpotRepository : IParkingSpotRepository
    {
        private readonly ParkingSpotContext _context;

        public ParkingSpotRepository(ParkingSpotContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ParkingSpot>> GetAllAsync()
        {
            return await _context.ParkingSpots.ToListAsync();
        }

        public async Task<ParkingSpot?> GetByIdAsync(int id)
        {
            return await _context.ParkingSpots.FindAsync(id);
        }

        public async Task<ParkingSpot> AddAsync(ParkingSpot spot)
        {
            await _context.ParkingSpots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return spot;
        }

        public async Task<ParkingSpot?> UpdateAsync(ParkingSpot spot)
        {
            var existingSpot = await _context.ParkingSpots.FindAsync(spot.id);
            if (existingSpot == null)
            {
                return null;
            }

            existingSpot.id = spot.id;
            existingSpot.location = spot.location;
            existingSpot.is_available = spot.is_available;

            _context.ParkingSpots.Update(existingSpot);
            await _context.SaveChangesAsync();

            return existingSpot;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var spot = await _context.ParkingSpots.FindAsync(id);
            if (spot == null)
            {
                return false;
            }

            _context.ParkingSpots.Remove(spot);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
