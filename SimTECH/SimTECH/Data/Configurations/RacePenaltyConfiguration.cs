using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class RacePenaltyConfiguration : IEntityTypeConfiguration<RacePenalty>
    {
        public void Configure(EntityTypeBuilder<RacePenalty> builder)
        {
            builder.HasKey(e => new { e.SeasonDriverId, e.RaceId, e.IncidentId });
        }
    }
}
