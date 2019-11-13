using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class Fitness : Procedure
    {
        public Fitness() : base()
        {

        }

        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);
            animal.Happiness -= 3;
            animal.Energy += 10;

            base.ProcedureHistory.Add(animal);
        }
    }
}
