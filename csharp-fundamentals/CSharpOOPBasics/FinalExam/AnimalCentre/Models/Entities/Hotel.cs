using AnimalCentre.Misc;
using AnimalCentre.Models.Contracts;
using System;
using System.Collections.Generic;

namespace AnimalCentre.Models.Entities
{
    public class Hotel : IHotel
    {
        private const int Capacity = 10;
        private Dictionary<string, IAnimal> animals;
        private Dictionary<string, List<string>> AdoptedAnimals;

        public Hotel()
        {
            this.animals = new Dictionary<string, IAnimal>();
            this.AdoptedAnimals = new Dictionary<string, List<string>>();
        }


        public IReadOnlyDictionary<string, IAnimal> Animals
        {
            get
            {
                return animals;
            }
        }

        public void Accommodate(IAnimal animal)
        {
            if (Animals.Count >= Capacity)
            {
                throw new InvalidOperationException(OutputMessages.NotEnoughCapacity);
            }

            if (Animals.ContainsKey(animal.Name))
            {
                throw new ArgumentException(string.Format(OutputMessages.NameAlreadyExistas, animal.Name));
            }

            animals.Add(animal.Name, animal);
        }

        public void Adopt(string animalName, string owner)
        {
            if (!Animals.ContainsKey(animalName))
            {
                throw new ArgumentException(string.Format(
                    OutputMessages.NameDoesNotExist, animalName));
            }

            Animals[animalName].Owner = owner;
            Animals[animalName].IsAdopt = true;
            animals.Remove(animalName);

            if (!AdoptedAnimals.ContainsKey(owner))
            {
                AdoptedAnimals.Add(owner, new List<string>());
            }

            AdoptedAnimals[owner].Add(animalName);
        }

        public IAnimal GetAnimal(string name)
        {
            if (!Animals.ContainsKey(name))
            {
                throw new ArgumentException(string.Format(OutputMessages.NameDoesNotExist, name));
            }

            return Animals[name];
        }

        internal Dictionary<string, List<string>> GetAdoptedAnimals()
        {
            return this.AdoptedAnimals;
        }
    }
}
