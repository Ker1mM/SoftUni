using Rider.Domain.Enums;
using Rider.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rider.Services.Race
{
    public class RaceLogic
    {
        private Bike Bike;
        private Track Track;
        private BikeStats BikeStats;
        private TrackStats TrackStats;
        private Random random;

        public RaceLogic(Bike bike, Track track)
        {
            this.Bike = bike;
            this.Track = track;
            this.BikeStats = RaceConstants.GetBikeStats(bike.Type);
            this.TrackStats = RaceConstants.GetTrackStats(track.Type);
            this.random = new Random();
        }
        public TimeSpan GetTime()
        {
            var time = GetTimeInSeconds();

            var hours = (int)(time / 3600);
            var minutes = (int)((time / 60) % 60);
            var seconds = (int)(time % 60);

            var result = new TimeSpan(hours, minutes, seconds);

            return result;
        }

        private double GetTimeInSeconds()
        {
            var finalSpeedRating = 1 + (GetBikeSpeedRating() * TrackStats.SpeedRating);
            var finalSuspensionRating = 1 + (GetBikeSuspensionRating() * TrackStats.SuspensionRating);
            var finalWeightRating = 1 + (GetBikeWeightRating() * TrackStats.WeightPenalty);
            var randomizer = random.Next(10, 200);

            var averageSpeed = TrackStats.AverageSpeed - (Track.Elevation * RaceConstants.ElevationConstant); //TODO: Tray to Combine weight with elevation

            averageSpeed += finalSpeedRating * 0.01;
            averageSpeed += finalSuspensionRating * 0.01;
            averageSpeed -= finalWeightRating;
            averageSpeed -= randomizer * 0.01;

            double averageSpeedInSeconds = averageSpeed / 3600.00;

            var totalTime = Track.Distance / averageSpeedInSeconds;

            return totalTime;
        }

        private double GetBikeSpeedRating()
        {
            var result = Bike.BikeParts
                .Sum(x => x.PlayerPart.Part.SpeedRating);

            return result * BikeStats.SpeedRating;
        }


        private double GetBikeSuspensionRating()
        {
            var result = Bike.BikeParts
                .Sum(x => x.PlayerPart.Part.SuspensionRating);

            return result * BikeStats.SuspensionRating;
        }

        private double GetBikeWeightRating()
        {
            var result = Bike.BikeParts
                .Sum(x => x.PlayerPart.Part.Weight);

            return result * BikeStats.WeightBonus;
        }


    }
}
