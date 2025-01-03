﻿using SimTECH.Common.Enums;
using SimTECH.Data.Models;
using SimTECH.PageModels.Traits;

namespace SimTECH.PageModels;

public class EntrantAssignee
{
    public EntrantAssignee(Driver driver)
    {
        Id = driver.Id;
        Name = driver.FullName;
        Country = driver.Country;
        Entrant = Entrant.Driver;

        if (driver.DriverTraits?.Any() == true)
        {
            foreach (var trait in driver.DriverTraits)
                InitialTraitIds.Add(trait.TraitId);
        }
    }
    public EntrantAssignee(Team team)
    {
        Id = team.Id;
        Name = team.Name;
        Country = team.Country;
        Entrant = Entrant.Team;

        if (team.TeamTraits?.Any() == true)
        {
            foreach (var trait in team.TeamTraits)
                InitialTraitIds.Add(trait.TraitId);
        }
    }
    public EntrantAssignee(Track track)
    {
        Id = track.Id;
        Name = track.Name;
        Country = track.Country;
        Entrant = Entrant.Track;

        if (track.TrackTraits?.Any() == true)
        {
            foreach (var trait in track.TrackTraits)
                InitialTraitIds.Add(trait.TraitId);
        }
    }

    public long Id { get; set; }
    public string Name { get; set; }
    public Country Country { get; set; }
    public Entrant Entrant { get; set; }

    public List<long> InitialTraitIds { get; set; } = [];

    public List<AssignableTrait> InitialTraits { get; set; } = [];
    public List<AssignableTrait> AssignedTraits { get; set; } = [];
    public List<AssignableTrait> RemovedTraits { get; set; } = [];

    public string GetAssignedText
    {
        get
        {
            if (AssignedTraits.Count == 0)
                return "-";

            var assignedNames = AssignedTraits.Select(e => e.Name).ToArray();

            return string.Join(", ", assignedNames);
        }
    }
}
