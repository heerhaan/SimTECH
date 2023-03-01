using SimTECH.Data.Models;

namespace SimTECH.Data.Services
{
    public class EngineService
    {
        private readonly List<Engine> _engines = new()
        {
            new Engine()
            {
                Id = 1,
                Name = "Honda",
                State = State.Active,
            },
        };

        public List<Engine> GetTestNames()
        {
            return _engines;
        }

        public void CreateEngine(Engine engine)
        {
            ValidateEngine(engine);

            _engines.Add(engine);
        }

        #region validation

        private static void ValidateEngine(Engine engine)
        {
            if (engine == null)
                throw new NullReferenceException("Engine is very null here, yes");
        }

        #endregion
    }
}
