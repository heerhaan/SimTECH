﻿namespace SimTECH.PageModels
{
    public class SeasonListModel
    {
        public long Id { get; set; }
        public int Year { get; set; }
        public State State { get; set; }

        public string League { get; set; }

        public string DriverName { get; set; }
        public Country DriverNationality { get; set; }

        public string TeamName { get; set; }
        public string TeamColour { get; set; }
        public string TeamAccent { get; set; }
        public Country TeamNationality { get; set; }
    }
}