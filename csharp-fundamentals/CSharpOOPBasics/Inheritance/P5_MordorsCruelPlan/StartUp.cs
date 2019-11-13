using P5_MordorsCruelPlan.Factories;

namespace P5_MordorsCruelPlan
{
    public class StartUp
    {
        static void Main()
        {
            string input = System.Console.ReadLine();
            string[] args = input.Split();

            int happinesPoints = 0;
            foreach (var item in args)
            {
                var food = FoodFactory.GetFood(item);
                happinesPoints += food.HappinessPoints;
            }

            var mood = MoodFactory.GetMood(happinesPoints);
            System.Console.WriteLine(happinesPoints);
            System.Console.WriteLine(mood.MoodStatus);
        }
    }
}
