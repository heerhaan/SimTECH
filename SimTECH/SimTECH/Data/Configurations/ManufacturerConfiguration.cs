using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(e => e.Colour)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength();

            builder.Property(e => e.Accent)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength();
        }
    }
}
