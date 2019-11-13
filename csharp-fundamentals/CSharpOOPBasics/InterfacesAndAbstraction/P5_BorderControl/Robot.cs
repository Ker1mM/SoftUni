namespace P5_BorderControl
{
    public class Robot : Traveller
    {
        public string Model { get; set; }

        public Robot(string model, string id) : base(id)
        {
            Model = model;
        }
    }
}
