using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    public class SeasonTeamConfiguration : IEntityTypeConfiguration<SeasonTeam>
    {
        public void Configure(EntityTypeBuilder<SeasonTeam> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Name)
                .HasColumnType("nvarchar(200)")
                .IsRequired();

            builder.Property(e => e.Principal)
                .HasColumnType("nvarchar(150)")
                .IsRequired();

            builder.Property(e => e.Colour)
                .HasMaxLength(9)
                .IsFixedLength()
                .IsRequired();

            builder.Property(e => e.Accent)
                .HasMaxLength(9)
                .IsFixedLength()
                .IsRequired();

            builder.HasOne(e => e.Season)
                .WithMany(e => e.SeasonTeams)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
