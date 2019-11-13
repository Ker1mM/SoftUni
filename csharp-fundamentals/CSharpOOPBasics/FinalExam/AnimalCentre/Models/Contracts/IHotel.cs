using System.Collections.Generic;

namespace AnimalCentre.Models.Contracts
{
    public interface IHotel
    {
        void Accommodate(IAnimal animal);
        void Adopt(string animalName, string owner);
    }
}
