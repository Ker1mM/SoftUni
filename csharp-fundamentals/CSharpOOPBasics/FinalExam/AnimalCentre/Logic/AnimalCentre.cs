using AnimalCentre.Models.Contracts;
using AnimalCentre.Models.Entities;
using AnimalCentre.Models.Entities.Animals;
using AnimalCentre.Models.Entities.Procedures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnimalCentre.Logic
{
    public class AnimalCentre
    {

        private Hotel animalHotel;
        private Chip chip;
        private DentalCare dentalCare;
        private Fitness fitness;
        private NailTrim nailTrim;
        private Play play;
        private Vaccinate vaccinate;

        public AnimalCentre()
        {
            this.animalHotel = new Hotel();
            this.chip = new Chip();
            this.fitness = new Fitness();
            this.dentalCare = new DentalCare();
            this.nailTrim = new NailTrim();
            this.play = new Play();
            this.vaccinate = new Vaccinate();
        }

        public string RegisterAnimal(string type, string name, int energy, int happiness, int procedureTime)
        {
            var animal = CreateAnimal(type, name, energy, happiness, procedureTime);
            this.animalHotel.Accommodate(animal);

            return $"Animal {name} registered successfully";
        }

        public string Chip(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            chip.DoService(animal, procedureTime);

            return $"{animal.Name} had chip procedure";
        }

        public string Vaccinate(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            vaccinate.DoService(animal, procedureTime);

            return $"{animal.Name} had vaccination procedure";
        }

        public string Fitness(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            fitness.DoService(animal, procedureTime);

            return $"{animal.Name} had fitness procedure";
        }

        public string Play(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            play.DoService(animal, procedureTime);

            return $"{animal.Name} was playing for {procedureTime} hours";
        }

        public string DentalCare(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            dentalCare.DoService(animal, procedureTime);

            return $"{animal.Name} had dental care procedure";
        }

        public string NailTrim(string name, int procedureTime)
        {
            var animal = animalHotel.GetAnimal(name);
            nailTrim.DoService(animal, procedureTime);

            return $"{animal.Name} had nail trim procedure";
        }

        public string Adopt(string animalName, string owner)
        {
            var animal = animalHotel.GetAnimal(animalName);
            animalHotel.Adopt(animalName, owner);

            if (animal.IsChipped)
            {
                return $"{owner} adopted animal with chip";
            }
            else
            {
                return $"{owner} adopted animal without chip";
            }
        }

        public string History(string type)
        {
            switch (type)
            {
                case "Chip":
                    return chip.History();
                case "DentalCare":
                    return dentalCare.History();
                case "Fitness":
                    return fitness.History();
                case "NailTrim":
                    return nailTrim.History();
                case "Play":
                    return play.History();
                case "Vaccinate":
                    return vaccinate.History();
                default:
                    throw new ArgumentException("Invalid type");
            }
        }



        private IAnimal CreateAnimal(string type, string name, int energy, int happiness, int procedureTime)
        {
            switch (type)
            {
                case "Cat":
                    return new Cat(name, energy, happiness, procedureTime);
                case "Dog":
                    return new Dog(name, energy, happiness, procedureTime);
                case "Lion":
                    return new Lion(name, energy, happiness, procedureTime);
                case "Pig":
                    return new Pig(name, energy, happiness, procedureTime);
                default:
                    throw new ArgumentException();
            }
        }

        public string End()
        {
            StringBuilder sb = new StringBuilder();

            var ownerList = animalHotel.GetAdoptedAnimals();

            ownerList = ownerList.OrderBy(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

            foreach (var owner in ownerList)
            {
                sb.AppendLine($"--Owner: {owner.Key}");
                sb.AppendLine($"    - Adopted animals: {string.Join(" ", owner.Value)}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
