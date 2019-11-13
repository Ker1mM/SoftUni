namespace P5_BorderControl
{
    public interface IDontKnow
    {
        string Id { get; }

        bool Check(string controlId);
    }
}
