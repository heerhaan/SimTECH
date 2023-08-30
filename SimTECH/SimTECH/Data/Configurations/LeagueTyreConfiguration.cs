using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class LeagueTyreConfiguration : IEntityTypeConfiguration<LeagueTyre>
{
    public void Configure(EntityTypeBuilder<LeagueTyre> builder)
    {
        builder.HasKey(e => new { e.LeagueId, e.TyreId });
    }
}
