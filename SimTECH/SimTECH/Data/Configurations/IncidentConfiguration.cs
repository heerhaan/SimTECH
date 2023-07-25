using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class IncidentConfiguration : IEntityTypeConfiguration<Incident>
    {
        public void Configure(EntityTypeBuilder<Incident> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Name).HasColumnType("nvarchar(200)").IsRequired();

            builder.Property(e => e.Colour)
                .HasMaxLength(9);
        }
    }
}
