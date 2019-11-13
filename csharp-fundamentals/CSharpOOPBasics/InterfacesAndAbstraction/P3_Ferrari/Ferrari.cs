namespace P3_Ferrari
{
    public class Ferrari : ICar
    {
        public string Name { get; set; }
        public string Model { get { return "488-Spider"; } }

        public Ferrari(string name)
        {
            Name = name;
        }

        public string Brake()
        {
            return "Brakes!";
        }

        public string Gas()
        {
            return "Zadu6avam sA!";
        }

        public override string ToString()
        {
            return Model + "/" + Brake() + "/" + Gas() + "/" + Name;
        }
    }
}
