using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class EngineConfiguration : IEntityTypeConfiguration<Engine>
    {
        public void Configure(EntityTypeBuilder<Engine> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(100)")
                .IsRequired();

            builder.Property(e => e.Colour)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength()
                .HasDefaultValue("#ffffffff");

            builder.Property(e => e.Accent)
                .IsRequired()
                .HasMaxLength(9)
                .IsFixedLength()
                .HasDefaultValue("#000000ff");
        }
    }
}
