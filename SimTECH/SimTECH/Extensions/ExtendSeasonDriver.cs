using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class ExtendSeasonDriver
    {
        public static int RetrieveStatusBonus(this SeasonDriver driver)
        {
            if (driver.TeamRole == TeamRole.Main)
                return 2;
            if (driver.TeamRole == TeamRole.Support)
                return -2;

            return 0;
        }
    }
}
