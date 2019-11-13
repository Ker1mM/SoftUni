using AnimalCentre.Models.Contracts;
using AnimalCentre.Models.Entities.Animals;
using System.Collections.Generic;
using System.Text;

namespace AnimalCentre.Models.Entities.Procedures
{
    public abstract class Procedure : IProcedure
    {

        private List<IAnimal> procedureHistory;


        protected Procedure()
        {
            this.ProcedureHistory = new List<IAnimal>();
        }

        public List<IAnimal> ProcedureHistory
        {
            get
            {
                return procedureHistory;
            }
            protected set
            {
                procedureHistory = value;
            }
        }

        public string History()
        {
            StringBuilder sb = new StringBuilder();

            string type = this.GetType().Name;
            sb.AppendLine($"{type}");
            foreach (var animal in this.ProcedureHistory)
            {
                sb.AppendLine($"    Animal type: {animal.GetType().Name} - {animal.Name} - Happiness: {animal.Happiness} - Energy: {animal.Energy}");
            }

            return sb.ToString().TrimEnd();
        }

        public abstract void DoService(IAnimal animal, int procedureTime);
    }
}
