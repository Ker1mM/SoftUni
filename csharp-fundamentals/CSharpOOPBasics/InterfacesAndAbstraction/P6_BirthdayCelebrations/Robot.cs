namespace P6_BirthdayCelebrations
{
    public class Robot
    {
        public string Model { get; set; }
        public string Id { get; set; }

        public Robot(string model, string id)
        {
            Id = id;
            Model = model;
        }
    }
}
