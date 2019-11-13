using System;
using System.Text;

namespace P6_Animals.Animals
{
    public abstract class Animal : ISoundProducable
    {
        protected string sound;
        private string name;
        private int age;
        private string gender;
        protected string type;

        public string Gender
        {
            get { return gender; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Invalid input!");
                }
                gender = value;
            }
        }

        public int Age
        {
            get { return age; }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Invalid input!");
                }
                age = value;
            }
        }

        public string Name
        {
            get { return name; }
            set
            {
                if (string.IsNullOrWhiteSpace(value) || value == string.Empty)
                {
                    throw new ArgumentException("Invalid input!");
                }
                name = value;
            }
        }

        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public virtual void ProduceSound()
        {
            System.Console.WriteLine(sound);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{type}\n");
            sb.Append($"{Name} {Age} {Gender}");
            return sb.ToString();
        }

        public void Print()
        {
            Console.WriteLine(ToString());
            ProduceSound();
        }
    }
}
