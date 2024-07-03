using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class DevelopmentLogConfiguration : IEntityTypeConfiguration<DevelopmentLog>
{
    public void Configure(EntityTypeBuilder<DevelopmentLog> builder)
    {
        builder.HasKey(e => e.Id);
    }
}
