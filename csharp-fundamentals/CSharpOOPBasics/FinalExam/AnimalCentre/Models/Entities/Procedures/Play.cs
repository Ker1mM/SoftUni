using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class Play : Procedure
    {
        public Play() : base()
        {

        }


        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);
            animal.Energy -= 6;
            animal.Happiness += 12;

            base.ProcedureHistory.Add(animal);
        }
    }
}
