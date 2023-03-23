using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class SeasonEngineConfiguration : IEntityTypeConfiguration<SeasonEngine>
    {
        public void Configure(EntityTypeBuilder<SeasonEngine> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.HasOne(e => e.Season)
                .WithMany(e => e.SeasonEngines)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
