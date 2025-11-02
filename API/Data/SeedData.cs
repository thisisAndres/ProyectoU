using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data;

public static class SeedData
{
    public static void Seed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Person>().HasData(
            new Person { Id = 1, FirstName = "Ana", LastName = "Torres", Email = "ana@correo.com" },
            new Person { Id = 2, FirstName = "Luis", LastName = "Pérez", Email = "luis@correo.com" }
        );

        modelBuilder.Entity<Vehicle>().HasData(
            new Vehicle { Id = 1, Plate = "ABC123", Brand = "Toyota", Model = "Yaris", OwnerId = 1 },
            new Vehicle { Id = 2, Plate = "XYZ789", Brand = "Hyundai", Model = "Accent", OwnerId = 2 },
            new Vehicle { Id = 3, Plate = "KLM456", Brand = "Honda", Model = "Civic", OwnerId = null }
        );
    }
}
