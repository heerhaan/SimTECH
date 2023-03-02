using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class StrategyTyreConfiguration : IEntityTypeConfiguration<StrategyTyre>
    {
        public void Configure(EntityTypeBuilder<StrategyTyre> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
