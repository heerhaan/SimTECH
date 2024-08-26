using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class DevelopmentRangeConfiguration : IEntityTypeConfiguration<DevelopmentRange>
{
    public void Configure(EntityTypeBuilder<DevelopmentRange> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
