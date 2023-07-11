namespace SimTECH.Data.Models
{
    public sealed class Engine : ModelState
    {
        public string Name { get; set; } = default!;

        public IList<SeasonEngine>? SeasonEngines { get; set; }
    }
}
