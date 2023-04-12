using SimTECH.Data.Models;

namespace SimTECH.Extensions
{
    public static class ExtendSeasonDriver
    {
        public static int RetrieveStatusBonus(this SeasonDriver driver, int modifier)
        {
            if (driver.TeamRole == TeamRole.Main)
                return modifier;
            if (driver.TeamRole == TeamRole.Support)
                return modifier * -1;

            return 0;
        }
    }
}
