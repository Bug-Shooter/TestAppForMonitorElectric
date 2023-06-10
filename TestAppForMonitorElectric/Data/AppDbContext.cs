using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;
using TestAppForMonitorElectric.Models;

namespace TestAppForMonitorElectric.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }
    }
}
