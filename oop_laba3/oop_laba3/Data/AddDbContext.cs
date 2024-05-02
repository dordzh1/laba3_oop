using Microsoft.EntityFrameworkCore;
using oop_laba3.Models;

namespace oop_laba3.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Human> Humen { get; set; }
        public DbSet<Parking> Parkings { get; set; }
        public DbSet<ParkingCar> ParkingCars { get; set; }
    }
}
