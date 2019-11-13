using System;

namespace DefiningClasses
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            Person person1 = new Person("Pesho", 20);

            Person person2 = new Person("Gosho", 18);
            string name = "Stamat";
            int age = 43;
            var person3 = new Person();
            person3.Name = name;
            person3.Age = age;
        }
    }
}
