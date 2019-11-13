using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class DentalCare : Procedure
    {
        public DentalCare() : base()
        {

        }

        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);
            animal.Happiness += 12;
            animal.Energy += 10;

            base.ProcedureHistory.Add(animal);
        }
    }
}
