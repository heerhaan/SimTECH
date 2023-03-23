using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class TeamTraitConfiguration : IEntityTypeConfiguration<TeamTrait>
    {
        public void Configure(EntityTypeBuilder<TeamTrait> builder)
        {
            builder.HasKey(e => new { e.TeamId, e.TraitId });
        }
    }
}
