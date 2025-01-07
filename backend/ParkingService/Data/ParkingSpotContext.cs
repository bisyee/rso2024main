using Microsoft.EntityFrameworkCore;
using ParkingService.Models;

namespace ParkingService.Data
{
    public class ParkingSpotContext : DbContext
    {
        public ParkingSpotContext(DbContextOptions<ParkingSpotContext> options) : base(options) { }

        public DbSet<ParkingSpot> parkingspots { get; set; }
        public DbSet<AllParkingSpots> allspots { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Reservation> reservations { get; set; }

         protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); 
        modelBuilder.Entity<User>().ToTable("users");
         modelBuilder.Entity<AllParkingSpots>().ToTable("allspots");
        modelBuilder.Entity<ParkingSpot>().ToTable("parkingspots");
        modelBuilder.Entity<Reservation>().ToTable("reservations");
    }

    }
}
