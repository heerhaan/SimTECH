using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class RaceClassConfiguration : IEntityTypeConfiguration<RaceClass>
    {
        public void Configure(EntityTypeBuilder<RaceClass> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(e => e.Tag)
                .HasColumnType("nvarchar(10)")
                .IsRequired();

            builder.Property(e => e.Colour)
                .HasMaxLength(9)
                .IsFixedLength()
                .IsRequired();
        }
    }
}
