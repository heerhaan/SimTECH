namespace SimTECH.Data.EditModels
{
    // NOTE: leaving this here as an idea/notation, there should be a way to have a generic record with id and state to be
    // changed, to a new class and then to return a valid instance of that same record
    // Problem is, how can we make a new instance of a generic object? interface? abstract base classes? what else?
    public class StateChanger
    {
        //private readonly BaseModel _model;

        public long Id { get; set; }
        public State State { get; set; }

        public StateChanger()
        {
            //Id = model.Id;
            //State = model.State;

            //_model = model;
        }
    }

    public interface IEntity
    {
        //public weSG(UriHostNameType[orASGuiophjbfageEAFG IBOPUHJ:NO])
    }
}
