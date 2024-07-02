using SimTECH.PageModels.RaceWeek;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimTECH.Tests.Data.Factories;

public static class RaceWeekFactory
{
    public static RaceDriver CreateRaceDriver(long id) => new()
    {
        SeasonDriverId = id,
        Status = Common.Enums.RaceStatus.Racing,
    };
}
