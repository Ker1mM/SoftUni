namespace P6_Animals.Animals
{
    public class Frog : Animal
    {
        public Frog(string name, int age, string gender) : base(name, age, gender)
        {
            base.sound = "Ribbit";
            base.type = "Frog";
        }
    }
}
