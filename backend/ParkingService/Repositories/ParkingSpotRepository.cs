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

        public async Task<IEnumerable<ParkingSpots>> GetAllAsync()
        {
            return await _context.parkingspots.ToListAsync();
        }

        public async Task<ParkingSpot?> GetByIdAsync(int id)
        {
            return await _context.ParkingSpots.FindAsync(id);
        }

         public async Task<ParkingSpots?> GetByNameAsync(string loc)
        {
            return await _context.parkingspots.FirstOrDefaultAsync(u => u.location == loc);
        }

        public async Task<ParkingSpot> AddAsync(ParkingSpot spot)
        {
            await _context.ParkingSpots.AddAsync(spot);
            await _context.SaveChangesAsync();
            return spot;
        }

        public async Task<string> ImportGeoJson(GeoJson geoJson)
        {
            foreach (var geometry in geoJson.geometries)
            {
                if (geometry.type == "Point")
                {
                    // Extract latitude and longitude from GeoJSON
                    var lat = geometry.coordinates[1];
                    var lon = geometry.coordinates[0];
                    string location1 = $"{lat},{lon}";

                    var existingSpot = await _context.parkingspots
                        .FirstOrDefaultAsync(ps => ps.location == location1);

                     if (existingSpot == null){
                        await _context.parkingspots.AddAsync(new ParkingSpots
                        {
                            location = location1,
                            totalspots = 40, // Default total spots
                            isavailable= true,
                            availablespots = 40, // Default available spots
                            type = "General",    // Default type
                            priceperhour = 2// Default price per hour
                        });}
                }
            }

            await _context.SaveChangesAsync();
            return "Parking spots imported successfully.";
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
