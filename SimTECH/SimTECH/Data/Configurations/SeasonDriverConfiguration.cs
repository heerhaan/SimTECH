using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class SeasonDriverConfiguration : IEntityTypeConfiguration<SeasonDriver>
{
    public void Configure(EntityTypeBuilder<SeasonDriver> builder)
    {
        builder.HasKey(e => e.Id);
        builder.HasOne(e => e.Season)
            .WithMany(e => e.SeasonDrivers)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
