using API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace API.Data.Configurations;

public class PersonConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.Property(p => p.FirstName).HasMaxLength(80).IsRequired();
        builder.Property(p => p.LastName).HasMaxLength(80).IsRequired();
        builder.Property(p => p.Email).HasMaxLength(160);
    }
}