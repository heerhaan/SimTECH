using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class ClimateConfiguration : IEntityTypeConfiguration<Climate>
    {
        public void Configure(EntityTypeBuilder<Climate> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Terminology).HasColumnType("nvarchar(100)").IsRequired();
            builder.Property(e => e.Icon).HasColumnType("varchar(max)").IsRequired();
        }
    }
}
