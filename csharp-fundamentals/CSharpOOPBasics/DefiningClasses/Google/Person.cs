using System.Collections.Generic;

namespace Google
{
    public class Person
    {
        private List<Parent> Parents;
        private List<Child> Children;
        private List<Pokemon> Pokemons;

        public string Name { get; set; }
        public Company Company { get; set; }
        public Car Car { get; set; }

        public Person(string name)
        {
            this.Name = name;
            this.Company = new Company();
            this.Car = new Car();
            this.Parents = new List<Parent>();
            this.Children = new List<Child>();
            this.Pokemons = new List<Pokemon>();
        }

        public void SetInfo(string[] info)
        {
            switch (info[1])
            {
                case "company":
                    this.Company = new Company(info[2], info[3], info[4]);
                    break;
                case "pokemon":
                    this.Pokemons.Add(new Pokemon(info[2], info[3]));
                    break;
                case "parents":
                    this.Parents.Add(new Parent(info[2], info[3]));
                    break;
                case "children":
                    this.Children.Add(new Child(info[2], info[3]));
                    break;
                case "car":
                    this.Car = new Car(info[2], info[3]);
                    break;
                default:
                    break;
            }
        }

        private string PrintPokemon()
        {
            string result = "Pokemon:\n";
            foreach (var pokemon in this.Pokemons)
            {
                result += pokemon.ToString() + "\n";
            }
            return result;
        }

        private string PrintParents()
        {
            string result = "Parents:\n";
            foreach (var parent in this.Parents)
            {
                result += parent.ToString() + "\n";
            }
            return result;
        }

        private string PrintChildren()
        {
            string result = "Children:\n";
            foreach (var child in this.Children)
            {
                result += child.ToString() + "\n";
            }
            return result;
        }

        public override string ToString()
        {
            return $"{this.Name}\n" +
                $"{this.Company.ToString()}\n" +
                $"{this.Car.ToString()}\n" +
                $"{PrintPokemon()}" +
                $"{PrintParents()}" +
                $"{PrintChildren()}";



        }

    }
}
