namespace P3_Ferrari
{
    public interface ICar
    {
        string Model { get; }
        string Name { get; }

        string Brake();
        string Gas();
    }
}
