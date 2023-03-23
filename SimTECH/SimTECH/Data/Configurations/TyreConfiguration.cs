using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class TyreConfiguration : IEntityTypeConfiguration<Tyre>
    {
        public void Configure(EntityTypeBuilder<Tyre> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(e => e.Colour)
                .HasMaxLength(9)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
