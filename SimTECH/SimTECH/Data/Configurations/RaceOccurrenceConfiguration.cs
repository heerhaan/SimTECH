using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class RaceOccurrenceConfiguration : IEntityTypeConfiguration<RaceOccurrence>
    {
        public void Configure(EntityTypeBuilder<RaceOccurrence> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
