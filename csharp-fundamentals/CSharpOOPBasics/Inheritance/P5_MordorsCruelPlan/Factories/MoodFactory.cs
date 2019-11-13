using P5_MordorsCruelPlan.Factories.Moods;

namespace P5_MordorsCruelPlan.Factories
{
    public static class MoodFactory
    {
        public static Mood GetMood(int points)
        {
            if (points < -5)
            {
                return new Angry();
            }
            else if (points <= 0)
            {
                return new Sad();
            }
            else if (points <= 15)
            {
                return new Happy();
            }
            else
            {
                return new JavaScript();
            }
        }
    }
}
