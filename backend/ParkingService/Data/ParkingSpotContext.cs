using Microsoft.EntityFrameworkCore;
using ParkingService.Models;

namespace ParkingService.Data
{
    public class ParkingSpotContext : DbContext
    {
        public ParkingSpotContext(DbContextOptions<ParkingSpotContext> options) : base(options) { }

        public DbSet<ParkingSpot> ParkingSpots { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Reservation> reservations { get; set; }

    }
}
