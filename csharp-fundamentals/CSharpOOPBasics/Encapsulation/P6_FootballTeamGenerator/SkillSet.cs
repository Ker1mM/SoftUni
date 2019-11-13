using System;

namespace P6_FootballTeamGenerator
{
    public class SkillSet
    {
        private int endurance;
        private int sprint;
        private int dribble;
        private int passing;
        private int shooting;

        public int Shooting
        {
            get { return shooting; }
            set
            {
                if (value < 0 || value > 100)
                {
                    System.Console.WriteLine("Shooting should be between 0 and 100.");
                }
                else
                {
                    this.shooting = value;
                }
            }
        }


        public int Passing
        {
            get { return passing; }
            set
            {
                if (value < 0 || value > 100)
                {
                    System.Console.WriteLine("Passing should be between 0 and 100.");
                }
                else
                {
                    this.passing = value;
                }
            }
        }


        public int Dribble
        {
            get { return dribble; }
            set
            {
                if (value < 0 || value > 100)
                {
                    System.Console.WriteLine("Dribble should be between 0 and 100.");
                }
                else
                {
                    this.dribble = value;
                }
            }
        }


        public int Sprint
        {
            get { return sprint; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Sprint should be between 0 and 100.");
                }
                else
                {
                    this.sprint = value;
                }
            }
        }


        public int Endurance
        {
            get { return endurance; }
            set
            {
                if (value < 0 || value > 100)
                {
                    throw new ArgumentException("Endurance should be between 0 and 100.");
                }
                else
                {
                    this.endurance = value;
                }
            }
        }

        public SkillSet(int endurance, int sprint, int dribble, int passing, int shooting)
        {
            this.Endurance = endurance;
            this.Sprint = sprint;
            this.Dribble = dribble;
            this.Passing = passing;
            this.Shooting = shooting;
        }

        public double GetAverageSkill()
        {
            double skill = this.Endurance + this.Sprint + this.Dribble + this.Passing + this.Shooting;
            skill /= 5.0;
            return skill;
        }
    }
}
