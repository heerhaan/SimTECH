using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class PracticeResultConfiguration : IEntityTypeConfiguration<PracticeScore>
    {
        public void Configure(EntityTypeBuilder<PracticeScore> builder)
        {
            builder.HasKey(t => t.Id);
            builder.Property(e => e.Scores)
                .HasColumnType("varchar(max)")
                .HasConversion(
                    e => string.Join(';', e ?? Array.Empty<int>()),
                    e => Array.ConvertAll(e.Split(';', StringSplitOptions.RemoveEmptyEntries), int.Parse));
        }
    }
}
