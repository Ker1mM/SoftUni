namespace P5_BorderControl
{
    public class Citizen : Traveller
    {
        public string Name { get; set; }
        public int Age { get; set; }

        public Citizen(string name, int age, string id) : base(id)
        {
            Name = name;
            Age = age;
        }
    }
}
