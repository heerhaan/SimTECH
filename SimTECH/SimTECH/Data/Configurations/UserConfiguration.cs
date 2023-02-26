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
            builder.HasKey(e => e.UserId);

            builder.Property(e => e.Username).HasColumnType("nvarchar(256)").IsRequired();

            builder.Ignore(e => e.ToClaimsPrincipal());
        }
    }
}
