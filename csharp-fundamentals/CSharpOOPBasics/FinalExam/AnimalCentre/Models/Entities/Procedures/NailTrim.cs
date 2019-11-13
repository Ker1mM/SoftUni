using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class NailTrim : Procedure
    {
        public NailTrim() : base()
        {

        }

        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);
            animal.Happiness -= 7;

            base.ProcedureHistory.Add(animal);
        }
    }
}
