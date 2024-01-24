using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.Configurations;

public class TrackConfiguration : IEntityTypeConfiguration<Track>
{
    public void Configure(EntityTypeBuilder<Track> builder)
    {
        builder.HasKey(x => x.Id);

        //builder.HasData(
        //    new Track { Id = 1, Name = "Spa-Francorchamps", Country = Country.BE, Length = 7.01, AeroMod = 0.55, ChassisMod = 1.1, PowerMod = 1.35, QualifyingMod = 0.7, DefenseMod = 0.9, State = State.Active },
        //    new Track { Id = 2, Name = "Circuit de Monaco", Country = Country.MO, Length = 3.05, AeroMod = 1.5, ChassisMod = 1.25, PowerMod = 0.50, QualifyingMod = 2.0, DefenseMod = 2.0, State = State.Active },
        //    new Track { Id = 3, Name = "Autodromo de Interlagos", Country = Country.BR, Length = 4.31, AeroMod = 0.85, ChassisMod = 1.05, PowerMod = 1.1, QualifyingMod = 0.9, DefenseMod = 0.8, State = State.Active },
        //    new Track { Id = 4, Name = "TT Assen", Country = Country.NL, Length = 4.55, AeroMod = 0.95, ChassisMod = 1.1, PowerMod = 0.95, QualifyingMod = 1.1, DefenseMod = 1.3, State = State.Active },
        //    new Track { Id = 5, Name = "Fuji Speedway", Country = Country.JP, Length = 5.99, AeroMod = 1.05, ChassisMod = 0.90, PowerMod = 1.05, QualifyingMod = 0.9, DefenseMod = 0.66, State = State.Active },
        //    new Track { Id = 6, Name = "Österreichring", Country = Country.AT, Length = 4.33, AeroMod = 1.05, ChassisMod = 0.95, PowerMod = 1.1, QualifyingMod = 0.8, DefenseMod = 1.0, State = State.Active },
        //    new Track { Id = 7, Name = "Autodromo Nazionale di Monza", Country = Country.IT, Length = 5.79, AeroMod = 0.8, ChassisMod = 0.75, PowerMod = 1.25, QualifyingMod = 1.2, DefenseMod = 1.2, State = State.Active },
        //    new Track { Id = 8, Name = "Sepang", Country = Country.FM, Length = 5.54, AeroMod = 1.1, ChassisMod = 0.9, PowerMod = 1.1, QualifyingMod = 1, DefenseMod = 0.9, State = State.Active }
        //);
    }
}
