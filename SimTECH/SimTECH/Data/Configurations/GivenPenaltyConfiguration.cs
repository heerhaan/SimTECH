using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class GivenPenaltyConfiguration : IEntityTypeConfiguration<GivenPenalty>
    {
        public void Configure(EntityTypeBuilder<GivenPenalty> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
