using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class PointAllotmentConfiguration : IEntityTypeConfiguration<PointAllotment>
    {
        public void Configure(EntityTypeBuilder<PointAllotment> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
