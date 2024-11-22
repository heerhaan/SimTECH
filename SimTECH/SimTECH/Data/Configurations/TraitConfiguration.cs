using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class TraitConfiguration : IEntityTypeConfiguration<Trait>
{
    public void Configure(EntityTypeBuilder<Trait> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name)
            .HasColumnType("nvarchar(150)")
            .IsRequired();

        builder.Property(e => e.Description);
    }
}
