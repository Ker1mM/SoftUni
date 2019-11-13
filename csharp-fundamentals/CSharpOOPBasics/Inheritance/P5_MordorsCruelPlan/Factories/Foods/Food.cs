namespace P5_MordorsCruelPlan.Factories.Foods
{
    public abstract class Food
    {
        private int happinessPoints;

        public int HappinessPoints
        {
            get => this.happinessPoints;
            private set
            {
                happinessPoints = value;
            }
        }

        public Food(int happinessPoints)
        {
            HappinessPoints = happinessPoints;
        }
    }
}
