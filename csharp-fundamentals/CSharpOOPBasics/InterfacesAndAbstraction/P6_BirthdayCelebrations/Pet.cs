namespace P6_BirthdayCelebrations
{
    public class Pet : IMatch
    {
        public string BirthDate { get; set; }
        public string Name { get; set; }

        public Pet(string name, string birthDate)
        {
            Name = name;
            BirthDate = birthDate;
        }

        public bool IsMatching(string controlBirthdate)
        {
            return BirthDate.EndsWith(controlBirthdate);
        }
    }
}
