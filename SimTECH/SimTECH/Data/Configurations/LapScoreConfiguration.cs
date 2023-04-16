using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class LapScoreConfiguration : IEntityTypeConfiguration<LapScore>
    {
        public void Configure(EntityTypeBuilder<LapScore> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
