namespace P5_MordorsCruelPlan.Factories.Moods
{
    public abstract class Mood
    {
        private string moodStatus;

        public string MoodStatus { get; private set; }

        public Mood(string status)
        {
            MoodStatus = status;
        }
    }
}
