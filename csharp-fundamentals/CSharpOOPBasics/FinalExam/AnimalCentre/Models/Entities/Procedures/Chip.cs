using AnimalCentre.Misc;
using AnimalCentre.Models.Contracts;
using System;

namespace AnimalCentre.Models.Entities.Procedures
{
    public class Chip : Procedure
    {
        public Chip() : base()
        {

        }

        public override void DoService(IAnimal animal, int procedureTime)
        {
            animal.SetProcedureTime(procedureTime);

            if (animal.IsChipped)
            {
                throw new ArgumentException(string.Format(OutputMessages.IsChipped, animal.Name));
            }

            animal.Happiness -= 5;
            animal.IsChipped = true;

            base.ProcedureHistory.Add(animal);
        }
    }
}
