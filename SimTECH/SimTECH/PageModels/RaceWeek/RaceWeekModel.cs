using SimTECH.Data.Models;

namespace SimTECH.PageModels.RaceWeek;

public class RaweCeekModel
{
    public Race Race { get; set; }// this one is fine
    public Climate Climate { get; set; }// also fine
    public Season Season { get; set; }// also fine
    public League League { get; set; }// also fine

    public List<RaweCeekDriver> RaweCeekDrivers { get; set; }// consider? set this per session

    public double GapMarge { get; set; }// might be fine? Consider setting all config variables in the model?
}
