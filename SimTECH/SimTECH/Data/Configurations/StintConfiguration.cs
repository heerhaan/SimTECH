using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class StintConfiguration : IEntityTypeConfiguration<Stint>
    {
        public void Configure(EntityTypeBuilder<Stint> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
