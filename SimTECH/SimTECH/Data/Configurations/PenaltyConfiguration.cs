using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class PenaltyConfiguration : IEntityTypeConfiguration<Penalty>
    {
        public void Configure(EntityTypeBuilder<Penalty> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Reason)
                .HasColumnType("varchar(50)")
                .IsRequired();
        }
    }
}
