using System;
using System.Collections.Generic;
using System.Text;

namespace Rider.Services.Race
{
    public class BikeStats
    {

        public double SuspensionRating { get; }

        public double SpeedRating { get; }

        public double WeightBonus { get; }

        public BikeStats(double suspensionRating, double speedRating, double weightBonus)
        {
            this.SpeedRating = speedRating;
            this.SuspensionRating = suspensionRating;
            this.WeightBonus = weightBonus;
        }

        public BikeStats()
        {

        }
    }
}
