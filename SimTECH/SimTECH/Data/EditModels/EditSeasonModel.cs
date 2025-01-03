﻿using SimTECH.Data.Models;
using SimTECH.Common.Enums;

namespace SimTECH.Data.EditModels;

public class EditSeasonModel
{
    private readonly Season _season;

    public long Id { get; set; }
    public State State { get; set; }
    public int Year { get; set; }

    public int MaximumDriversInRace { get; set; } = 99;
    public int QualifyingAmountQ2 { get; set; } = 15;
    public int QualifyingAmountQ3 { get; set; } = 10;
    public int QualifyingRNG { get; set; } = 50;
    public int RunAmountSession { get; set; } = 2;
    public int GridBonus { get; set; } = 25;
    public int PitMinimum { get; set; } = 100;
    public int PitMaximum { get; set; } = 300;
    public int PitCostSubtractCaution { get; set; } = 100;
    public int RngMinimum { get; set; }
    public int RngMaximum { get; set; } = 50;
    public int MistakeRolls { get; set; } = 1;
    public int MistakeMinimum { get; set; } = 150;
    public int MistakeMaximum { get; set; } = 300;
    public int PointsPole { get; set; }
    public int PointsFastestLap { get; set; }
    public QualyFormat QualifyingFormat { get; set; }
    public long LeagueId { get; set; }
    public IList<EditPointAllotmentModel> PointAllotments { get; set; } = new List<EditPointAllotmentModel>();
    public IList<EditRaceClassModel> RaceClasses { get; set; } = new List<EditRaceClassModel>();

    public EditSeasonModel(Season? season)
    {
        if (season == null)
        {
            _season = new Season();
        }
        else
        {
            Id = season.Id;
            State = season.State;
            Year = season.Year;
            MaximumDriversInRace = season.MaximumDriversInRace;
            QualifyingAmountQ2 = season.QualifyingAmountQ2;
            QualifyingAmountQ3 = season.QualifyingAmountQ3;
            QualifyingRNG = season.QualifyingRNG;
            RunAmountSession = season.RunAmountSession;
            GridBonus = season.GridBonus;
            PitMinimum = season.PitMinimum;
            PitMaximum = season.PitMaximum;
            PitCostSubtractCaution = season.PitCostSubtractCaution;
            RngMinimum = season.RngMinimum;
            RngMaximum = season.RngMaximum;
            MistakeRolls = season.MistakeRolls;
            MistakeMinimum = season.MistakeMinimum;
            MistakeMaximum = season.MistakeMaximum;
            PointsPole = season.PointsPole;
            PointsFastestLap = season.PointsFastestLap;
            QualifyingFormat = season.QualifyingFormat;
            LeagueId = season.LeagueId;

            if (season.PointAllotments != null)
                PointAllotments = season.PointAllotments.Select(e => new EditPointAllotmentModel(e)).ToList();
            if (season.RaceClasses?.Any() == true)
                RaceClasses = season.RaceClasses.Select(e => new EditRaceClassModel(e)).ToList();

            _season = season;
        }
    }

    public Season Record =>
        new()
        {
            Id = Id,
            State = State,
            Year = Year,
            MaximumDriversInRace = MaximumDriversInRace,
            QualifyingFormat = QualifyingFormat,
            QualifyingAmountQ2 = QualifyingAmountQ2,
            QualifyingAmountQ3 = QualifyingAmountQ3,
            QualifyingRNG = QualifyingRNG,
            RunAmountSession = RunAmountSession,
            GridBonus = GridBonus,
            PitMinimum = PitMinimum,
            PitMaximum = PitMaximum,
            PitCostSubtractCaution = PitCostSubtractCaution,
            RngMinimum = RngMinimum,
            RngMaximum = RngMaximum,
            MistakeRolls = MistakeRolls,
            MistakeMinimum = MistakeMinimum,
            MistakeMaximum = MistakeMaximum,
            PointsPole = PointsPole,
            PointsFastestLap = PointsFastestLap,
            LeagueId = LeagueId,

            PointAllotments = PointAllotments.Select(e => e.Record).ToList(),
            RaceClasses = RaceClasses.Select(e => e.Record).ToList()
        };

    public bool IsDirty => _season != Record || PointAllotments.Any(e => e.IsDirty);

    public void ResetIdentifierFields()
    {
        Id = default;
        State = State.Concept;

        foreach (var point in PointAllotments)
            point.ResetIdentifierFields();

        foreach (var raceClasss in RaceClasses)
            raceClasss.ResetIdentifierFields();
    }
}

public class EditPointAllotmentModel
{
    private readonly PointAllotment _pointAllotment;

    public long Id { get; set; }
    public int Position { get; set; }
    public int Points { get; set; }

    public EditPointAllotmentModel() { _pointAllotment = new PointAllotment(); }
    public EditPointAllotmentModel(PointAllotment pointAllotment)
    {
        Id = pointAllotment.Id;
        Position = pointAllotment.Position;
        Points = pointAllotment.Points;

        _pointAllotment = pointAllotment;
    }

    public PointAllotment Record =>
        new()
        {
            Id = Id,
            Position = Position,
            Points = Points,
        };

    public bool IsDirty => _pointAllotment != Record;

    public void ResetIdentifierFields()
    {
        Id = default;
    }
}

public class EditRaceClassModel
{
    private readonly RaceClass _raceClass;

    public long Id { get; set; }
    public string Name { get; set; }
    public string Colour { get; set; }
    public string Tag { get; set; }

    public EditRaceClassModel() { _raceClass = new(); }
    public EditRaceClassModel(RaceClass raceClass)
    {
        Id = raceClass.Id;
        Name = raceClass.Name;
        Colour = raceClass.Colour;
        Tag = raceClass.Tag;

        _raceClass = raceClass;
    }

    public RaceClass Record =>
        new()
        {
            Id = Id,
            Name = Name,
            Colour = Colour,
            Tag = Tag
        };

    public bool IsDirty => _raceClass != Record;

    public void ResetIdentifierFields()
    {
        Id = default;
    }
}
