using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class QualifyingResultConfiguration : IEntityTypeConfiguration<QualifyingScore>
    {
        public void Configure(EntityTypeBuilder<QualifyingScore> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(e => e.Scores)
                .HasConversion(
                    e => string.Join(';', e ?? Array.Empty<int>()),
                    e => Array.ConvertAll(e.Split(';', StringSplitOptions.RemoveEmptyEntries), int.Parse));
        }
    }
}
