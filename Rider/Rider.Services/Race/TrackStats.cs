using System;
using System.Collections.Generic;
using System.Text;

namespace Rider.Services.Race
{
    public class TrackStats
    {

        public double SuspensionRating { get; }

        public double SpeedRating { get; }

        public double WeightPenalty { get; }

        public double AverageSpeed { get; }

        public TrackStats()
        {

        }

        public TrackStats(double suspensionRating, double speedRating, double weightPenalty, double averageSpeed)
        {
            this.SpeedRating = speedRating;
            this.SuspensionRating = suspensionRating;
            this.WeightPenalty = weightPenalty;
            this.AverageSpeed = averageSpeed;
        }
    }
}
