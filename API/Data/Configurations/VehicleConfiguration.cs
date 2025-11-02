using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;

public class VehicleConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.Property(v => v.Plate).HasMaxLength(8).IsRequired();
        builder.Property(v => v.Brand).HasMaxLength(60).IsRequired();
        builder.Property(v => v.Model).HasMaxLength(60).IsRequired();

        builder.HasIndex(v => v.Plate).IsUnique();

        builder.HasOne(v => v.Owner)
               .WithMany(p => p.Vehicles)
               .HasForeignKey(v => v.OwnerId)
               .OnDelete(DeleteBehavior.SetNull);
    }
}
