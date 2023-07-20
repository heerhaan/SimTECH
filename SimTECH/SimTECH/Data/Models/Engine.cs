namespace SimTECH.Data.Models
{
    public sealed class Engine : ModelState
    {
        public string Name { get; set; } = default!;
        public bool Mark { get; set; }

        public IList<SeasonEngine>? SeasonEngines { get; set; }
    }
}
