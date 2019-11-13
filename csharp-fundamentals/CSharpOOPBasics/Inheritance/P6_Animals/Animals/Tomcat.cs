namespace P6_Animals.Animals
{
    public class Tomcat : Cat
    {
        private const string gender = "Male";
        public Tomcat(string name, int age, string tempGender) : base(name, age, gender)
        {
            base.sound = "MEOW";
            base.type = "Tomcat";
        }
    }
}
