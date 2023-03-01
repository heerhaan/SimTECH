using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class DriverConfiguration : IEntityTypeConfiguration<Driver>
    {
        public void Configure(EntityTypeBuilder<Driver> builder)
        {
            builder.HasKey(e => e.Id);
            builder.Property(e => e.FirstName).HasColumnType("nvarchar(100)");
            builder.Property(e => e.LastName).HasColumnType("nvarchar(150)");
            builder.Property(e => e.Abbreviation).HasColumnType("nvarchar(20)");
            builder.Property(e => e.DateOfBirth).HasColumnType("date");
            builder.Property(e => e.Country).HasColumnType("varchar(10)");
            builder.Property(e => e.Biography).HasColumnType("nvarchar(max)");
        }
    }
}
