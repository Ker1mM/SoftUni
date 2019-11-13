using AnimalCentre.Models.Contracts;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class Vaccinate : Procedure
    {
        public Vaccinate() : base()
        {

        }


        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);
            animal.Energy -= 8;
            animal.IsVaccinated = true;

            base.ProcedureHistory.Add(animal);
        }
    }
}

