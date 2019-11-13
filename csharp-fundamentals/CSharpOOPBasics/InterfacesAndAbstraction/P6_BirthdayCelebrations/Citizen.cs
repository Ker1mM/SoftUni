namespace P6_BirthdayCelebrations
{
    public class Citizen : IMatch
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public string BirthDate { get; set; }

        public Citizen(string name, int age, string id, string birthDate)
        {
            Id = id;
            Name = name;
            Age = age;
            BirthDate = birthDate;
        }

        public bool IsMatching(string controlBirthdate)
        {
            return BirthDate.EndsWith(controlBirthdate);
        }
    }
}
