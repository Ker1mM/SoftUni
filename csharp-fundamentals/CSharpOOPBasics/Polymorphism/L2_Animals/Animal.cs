namespace Animals
{

    public class Animal
    {
        public string Name { get; set; }
        public string FavoriteFood { get; set; }

        public virtual string ExplainSelf()
        {
            return $"I am {Name} and my fovourite food is {FavoriteFood}";
        }

        public Animal(string name, string food)
        {
            Name = name;
            FavoriteFood = food;
        }
    }
}
