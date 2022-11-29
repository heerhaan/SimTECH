namespace SimTECH
{
    public class ThingState
    {
        public bool ShowingDialog { get; private set; }

        public Thing ConfiguringThing { get; private set; }
        public StuffThang TStuff { get; private set; } = new StuffThang();

        public void ShowThingDialog()
        {
            ConfiguringThing = new Thing()
            {
                Name = "Initial name",
                CoolPoints = 1,
                IsCool = false
            };

            ShowingDialog = true;
        }

        public void CancelThingDialog()
        {
            ConfiguringThing = null;
            ShowingDialog = false;
        }

        public void ConfirmThingDialog()
        {
            TStuff.Things.Add(ConfiguringThing);

            ConfiguringThing = null;
            ShowingDialog = false;
        }
    }

    public class StuffThang
    {
        public List<Thing> Things { get; set; }
    }

    public class Thing
    {
        public string Name { get; set; }
        public int CoolPoints { get; set; }
        public bool IsCool { get; set; }
    }
}
