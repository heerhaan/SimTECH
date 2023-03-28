﻿using SimTECH.Data.Models;

namespace SimTECH.PageModels
{
    public abstract class DriverBase
    {
        public long ResultId { get; set; }

        public string FullName { get; set; }
        public int Number { get; set; }
        public TeamRole Role { get; set; }
        public Country Nationality { get; set; }

        public string TeamName { get; set; }
        public string Colour { get; set; }
        public string Accent { get; set; }

        public int Power { get; set; }

        public TraitEffect TraitEffect { get; set; }
    }

    public class RaceDriver : DriverBase
    {
        public Result Result { get; set; }//overwegend weg ermee?

        public int[] RunValues { get; set; }//weg ermee... toch?

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunValues.Max();//weg ermee... toch?... alhoewel FL?
    }

    public class QualifyingDriver : DriverBase
    {
        // We now repeat these properties three times, enforcing (for now) three sessions but it's not too hard to make a list of this
        public int[] RunValuesQ1 { get; set; }
        public int PositionQ1 { get; set; }
        public int MaxScoreQ1 => RunValuesQ1.Max();

        public int[] RunValuesQ2 { get; set; }
        public int PositionQ2 { get; set; }
        public int MaxScoreQ2 => RunValuesQ2.Max();

        public int[] RunValuesQ3 { get; set; }
        public int PositionQ3 { get; set; }
        public int MaxScoreQ3 => RunValuesQ3.Max();
    }

    public class PracticeDriver : DriverBase
    {
        public int[] RunValues { get; set; }

        // Properties underneath are optionable, depending on circumstances
        public int Position { get; set; }
        public int Score { get; set; }

        public int MaxScore => RunValues.Max();
    }
}
