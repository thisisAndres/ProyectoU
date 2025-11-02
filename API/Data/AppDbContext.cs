using Microsoft.EntityFrameworkCore;
using API.Models;

namespace API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        // DbSets:
        public DbSet<Person> Persons => Set<Person>();
        public DbSet<Vehicle> Vehicles => Set<Vehicle>();
    }
}
