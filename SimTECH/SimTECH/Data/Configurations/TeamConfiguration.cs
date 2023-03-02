using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(e => e.Country)
                .HasColumnType("varchar(2)")
                .IsRequired()
                .HasMaxLength(2);

            builder.Property(e => e.Biography)
                .HasColumnType("nvarchar(max)");
        }
    }
}
