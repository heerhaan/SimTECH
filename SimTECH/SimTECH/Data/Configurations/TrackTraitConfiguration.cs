using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class TrackTraitConfiguration : IEntityTypeConfiguration<TrackTrait>
    {
        public void Configure(EntityTypeBuilder<TrackTrait> builder)
        {
            builder.HasKey(e => new { e.TrackId, e.TraitId });
        }
    }
}
