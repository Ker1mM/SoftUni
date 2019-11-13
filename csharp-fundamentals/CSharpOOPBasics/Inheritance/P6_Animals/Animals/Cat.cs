namespace P6_Animals.Animals
{
    public class Cat : Animal
    {
        public Cat(string name, int age, string gender) : base(name, age, gender)
        {
            base.sound = "Meow meow";
            base.type = "Cat";
        }
    }
}
