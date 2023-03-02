﻿namespace SimTECH.Data.Models
{
    public class Team
    {
        public long Id { get; set; }
        public string Name { get; set; } = default!;
        public Country Country { get; set; } = default!;
        public string Biography { get; set; } = default!;
        public State State { get; set; }

        // We need a very worked out design for how finances will be used and how to implement it in such a way that it can work automatically too
        // Thus far sponsors each season are a swell idea, (so part of SeasonTeam). But how will the money be used, contracts?
        //public int Balance { get; set; }//the fuck is balance, dat is geld mika

        public IList<Contract> Contracts { get; set; } = default!;
        public IList<SeasonTeam> SeasonTeams { get; set; } = default!;
        public IList<TeamTrait> TeamTraits { get; set; } = default!;
    }
}