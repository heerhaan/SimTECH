using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class PracticeResultConfiguration : IEntityTypeConfiguration<PracticeScore>
    {
        public void Configure(EntityTypeBuilder<PracticeScore> builder)
        {
            builder.HasKey(t => t.Id);

            var scoreValueComparer = new ValueComparer<int[]>(
                (c1, c2) => c1!.SequenceEqual(c2 ?? Array.Empty<int>()),
                c => c.Aggregate(0, (a, v) => HashCode.Combine(a, v.GetHashCode())),
                c => c.ToArray());

            builder.Property(e => e.Scores)
                .HasConversion(
                    e => string.Join(';', e ?? Array.Empty<int>()),
                    e => Array.ConvertAll(e.Split(';', StringSplitOptions.RemoveEmptyEntries), int.Parse))
                .Metadata
                .SetValueComparer(scoreValueComparer);
        }
    }
}
