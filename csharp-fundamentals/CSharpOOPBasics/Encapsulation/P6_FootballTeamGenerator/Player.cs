using System;

namespace P6_FootballTeamGenerator
{
    public class Player
    {
        private string name;
        private SkillSet skills;

        public SkillSet Skills
        {
            get => this.skills;
        }


        public string Name
        {
            get { return name; }
            set
            {
                if (value == null || value == "" || value == " ")
                {
                    throw new ArgumentException("A name should not be empty.");
                }
                else
                {
                    this.name = value;
                }
            }
        }

        public Player(string name, int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Name = name;
            this.skills = new SkillSet(endurance, sprint, dribble, passing, shooting);
        }

    }
}
