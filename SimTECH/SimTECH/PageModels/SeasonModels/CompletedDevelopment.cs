﻿namespace SimTECH.PageModels.SeasonModels
{
    public class CompletedDevelopment
    {
        public CompletedDevelopment(Entrant group, Aspect aspect, int min, int max, List<DevelopedEntrant> entrants)
        {
            EntrantGroup = group;
            DevelopedAspect = aspect;
            MinChange = min;
            MaxChange = max;
            Entrants = entrants?.ToArray() ?? Array.Empty<DevelopedEntrant>();
        }

        public Entrant EntrantGroup { get; set; }
        public Aspect DevelopedAspect { get; set; }
        public int MinChange { get; set; }
        public int MaxChange { get; set; }
        public DevelopedEntrant[] Entrants { get; set; }
    }
}
