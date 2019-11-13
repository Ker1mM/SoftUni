namespace P6_BirthdayCelebrations
{
    public interface IMatch
    {
        string BirthDate { get; }
        bool IsMatching(string controlDate);
    }
}
