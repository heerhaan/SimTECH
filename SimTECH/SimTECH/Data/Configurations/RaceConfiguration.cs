using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class RaceConfiguration : IEntityTypeConfiguration<Race>
    {
        public void Configure(EntityTypeBuilder<Race> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();
        }
    }
}
