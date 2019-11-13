using AnimalCentre.Misc;
using AnimalCentre.Models.Contracts;
using System;

namespace AnimalCentre.Models.Entities.Animals
{
    public abstract class Animal : IAnimal
    {
        private int happiness;
        private int energy;

        public string Name { get; private set; }
        public int ProcedureTime { get; private set; }
        public string Owner { get; set; }
        public bool IsAdopt { get; set; }
        public bool IsChipped { get; set; }
        public bool IsVaccinated { get; set; }

        protected Animal(string name, int energy, int happiness, int procedureTime)
        {
            this.Name = name;
            this.Energy = energy;
            this.Happiness = happiness;
            this.ProcedureTime = procedureTime;
            this.Owner = "Centre";
            this.IsAdopt = false;
            this.IsChipped = false;
            this.IsVaccinated = false;
        }

        public int Energy
        {
            get { return energy; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(OutputMessages.InvalidEnergy);
                }
                energy = value;
            }
        }


        public int Happiness
        {
            get { return happiness; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException(OutputMessages.InvalidHappiness);
                }
                happiness = value;
            }
        }

        public abstract override string ToString();

        public void SetProcedureTime(int procedureTime)
        {
            if (procedureTime > this.ProcedureTime)
            {
                throw new ArgumentException(OutputMessages.NotEnoughProcedureTime);
            }

            this.ProcedureTime -= procedureTime;
        }

    }
}
