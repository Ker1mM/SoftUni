namespace FestivalManager.Entities.Instruments
{
    public class Drums : Instrument
    {
        public Drums() : base()
        {

        }
        protected override int RepairAmount => 20;
    }
}
