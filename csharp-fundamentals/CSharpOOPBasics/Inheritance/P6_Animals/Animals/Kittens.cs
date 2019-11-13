namespace P6_Animals.Animals
{
    public class Kittens : Cat
    {
        private const string gender = "Female";
        public Kittens(string name, int age, string tempGender) : base(name, age, gender)
        {
            base.sound = "Meow";
            base.type = "Kitten";
        }
    }
}
