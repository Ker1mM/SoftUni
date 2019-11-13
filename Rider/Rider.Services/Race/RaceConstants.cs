using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Rider.Services.Race
{
    public static class RaceConstants
    {
        public const double ElevationConstant = 0.002;

        private const double RoadTrackAverageSpeed = 46;
        private const double GravelTrackAverageSpeed = 41;
        private const double XCTrackAverageSpeed = 36;
        private const double DHTrackAverageSpeed = 36;

        private const double RoadBikeSuspension = 1.10;
        private const double HMTBSuspension = 1.50;
        private const double FSMTBSuspension = 2;
        private const double CityBikeSuspension = 1.20;
        private const double EBikeSuspension = 1.30;
        private const double GravelBikeSuspension = 1.30;

        private const double RoadBikeSpeed = 2;
        private const double HMTBSpeed = 1.60;
        private const double FSMTBSpeed = 1.20;
        private const double CityBikeSpeed = 1.40;
        private const double EBikeSpeed = 1.60;
        private const double GravelBikeSpeed = 1.80;

        private const double RoadBikeWeightBonus = 0.06;
        private const double HMTBWeightBonus = 0.1;
        private const double FSMTBWeightBonus = 0.13;
        private const double CityBikeWeightBonus = 0.11;
        private const double EBikeWeightBonus = 0.15;
        private const double GravelBikeWeightBonus = 0.08;

        private const double RoadTrackSpeedRating = 1;
        private const double RoadTrackSuspensionRating = 0.1;
        private const double RoadTrackWeightBonus = 0.009;

        private const double GravelTrackSpeedRating = 0.8;
        private const double GravelTrackSuspensionRating = 0.3;
        private const double GravelTrackWeightBonus = 0.007;

        private const double XCTrackSpeedRating = 0.6;
        private const double XCTrackSuspensionRating = 0.4;
        private const double XCTrackWeightBonus = 0.005;

        private const double DHTrackSpeedRating = 0.3;
        private const double DHTrackSuspensionRating = 1;
        private const double DHTrackWeightBonus = 0.003;



        public static TrackStats GetTrackStats(TrackType trackType)
        {
            var stats = new TrackStats();

            switch (trackType)
            {
                case TrackType.Road:
                    stats = new TrackStats(RoadTrackSuspensionRating, RoadTrackSpeedRating, RoadTrackWeightBonus, RoadTrackAverageSpeed);
                    break;

                case TrackType.XC:
                    stats = new TrackStats(XCTrackSuspensionRating, XCTrackSpeedRating, XCTrackWeightBonus, XCTrackAverageSpeed);
                    break;

                case TrackType.Gravel:
                    stats = new TrackStats(GravelTrackSuspensionRating, GravelTrackSpeedRating, GravelTrackWeightBonus, GravelTrackAverageSpeed);
                    break;

                case TrackType.DH:
                    stats = new TrackStats(DHTrackSuspensionRating, DHTrackSpeedRating, DHTrackWeightBonus, DHTrackAverageSpeed);
                    break;

                default:
                    break;
            }

            return stats;
        }

        public static BikeStats GetBikeStats(BikeType bikeType)
        {
            var stats = new BikeStats();

            switch (bikeType)
            {
                case BikeType.Road:
                    stats = new BikeStats(RoadBikeSuspension, RoadBikeSpeed, RoadBikeWeightBonus);
                    break;

                case BikeType.HMTB:
                    stats = new BikeStats(HMTBSuspension, HMTBSpeed, HMTBWeightBonus);
                    break;

                case BikeType.Gravel:
                    stats = new BikeStats(GravelBikeSuspension, GravelBikeSpeed, GravelBikeWeightBonus);
                    break;

                case BikeType.FSMTB:
                    stats = new BikeStats(FSMTBSuspension, FSMTBSpeed, FSMTBWeightBonus);
                    break;

                case BikeType.EBike:
                    stats = new BikeStats(EBikeSuspension, EBikeSpeed, EBikeWeightBonus);
                    break;

                case BikeType.City:
                    stats = new BikeStats(CityBikeSuspension, CityBikeSpeed, CityBikeWeightBonus);
                    break;

                default:
                    break;
            }

            return stats;
        }
    }
}
