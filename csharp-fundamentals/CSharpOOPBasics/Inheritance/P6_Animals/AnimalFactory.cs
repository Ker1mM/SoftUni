using P6_Animals.Animals;
using System;

namespace P6_Animals
{
    public static class AnimalFactory
    {
        public static Animal GetAnimal(string name, int age, string gender, string type)
        {
            switch (type)
            {
                case "Cat":
                    return new Cat(name, age, gender);
                case "Dog":
                    return new Dog(name, age, gender);
                case "Frog":
                    return new Frog(name, age, gender);
                case "Kitten":
                    return new Kittens(name, age, gender);
                case "Tomcat":
                    return new Tomcat(name, age, gender);
                default:
                    throw new ArgumentException("Invalid input!");
            }
        }
    }
}
