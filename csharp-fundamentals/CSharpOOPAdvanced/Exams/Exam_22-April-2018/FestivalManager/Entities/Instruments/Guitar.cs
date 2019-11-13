namespace FestivalManager.Entities.Instruments
{
    public class Guitar : Instrument
    {
        public Guitar() : base()
        {

        }
        protected override int RepairAmount => 60;
    }
}
