using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations
{
    // ADVICE: this class can (and should!) be used for validation
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Username)
                .HasColumnType("nvarchar(256)")
                .IsRequired();

            builder.Property(e => e.Password)
                .HasColumnType("nvarchar(max)")
                .IsRequired();
        }
    }
}
