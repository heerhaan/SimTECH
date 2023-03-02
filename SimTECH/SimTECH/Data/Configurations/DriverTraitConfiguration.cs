using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class DriverTraitConfiguration : IEntityTypeConfiguration<DriverTrait>
    {
        public void Configure(EntityTypeBuilder<DriverTrait> builder)
        {
            builder.HasKey(e => new { e.DriverId, e.TraitId });
        }
    }
}
