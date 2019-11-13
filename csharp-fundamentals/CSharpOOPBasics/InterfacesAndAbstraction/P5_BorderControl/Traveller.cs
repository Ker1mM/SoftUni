namespace P5_BorderControl
{
    public abstract class Traveller : IDontKnow
    {
        public string Id { get; set; }

        public Traveller(string id)
        {
            Id = id;
        }

        public bool Check(string controlId)
        {
            return Id.EndsWith(controlId);
        }
    }
}
