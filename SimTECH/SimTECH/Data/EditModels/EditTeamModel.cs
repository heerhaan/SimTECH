﻿using SimTECH.Common.Constants;
using SimTECH.Common.Enums;
using SimTECH.Data.Models;

namespace SimTECH.Data.EditModels;

public sealed class EditTeamModel
{
    private readonly Team _team;

    public long Id { get; set; }
    public string? Name { get; set; }
    public Country Country { get; set; } = Globals.DefaultCountry;
    public string? Biography { get; set; }
    public bool Mark { get; set; }
    public State State { get; set; }

    public IList<EditTeamTraitModel> TeamTraits { get; set; } = new List<EditTeamTraitModel>();

    public EditTeamModel(Team? team)
    {
        if (team == null)
        {
            _team = new();
        }
        else
        {
            Id = team.Id;
            Name = team.Name;
            Country = team.Country;
            Biography = team.Biography;
            Mark = team.Mark;
            State = team.State;

            if (team.TeamTraits != null)
                TeamTraits = team.TeamTraits.Select(e => new EditTeamTraitModel(e)).ToList();

            _team = team;
        }
    }

    public Team Record =>
        new()
        {
            Id = Id,
            Name = Name ?? "Unknown",
            Country = Country,
            Biography = Biography ?? string.Empty,
            Mark = Mark,
            State = State,

            TeamTraits = TeamTraits.Select(e => e.Record).ToList()
        };

    public bool IsDirty => _team != Record || TeamTraits.Any(e => e.IsDirty);
}

public sealed class EditTeamTraitModel
{
    private readonly TeamTrait _teamTrait;

    public long TeamId { get; set; }
    public long TraitId { get; set; }

    public EditTeamTraitModel() { _teamTrait = new TeamTrait(); }
    public EditTeamTraitModel(TeamTrait teamTrait)
    {
        TeamId = teamTrait.TeamId;
        TraitId = teamTrait.TraitId;

        _teamTrait = teamTrait;
    }

    public TeamTrait Record =>
        new()
        {
            TeamId = TeamId,
            TraitId = TraitId
        };

    public bool IsDirty => _teamTrait != Record;
}
