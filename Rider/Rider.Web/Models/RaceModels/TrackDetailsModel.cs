using Rider.Web.Areas.Cved.Models;
using Rider.Web.Models.BikeModels;
using System.Collections.Generic;

namespace Rider.Web.Models.RaceModels
{
    public class TrackDetailsModel
    {
        public TrackViewModel Track { get; set; }

        public IEnumerable<AttemptViewModel> Attempts { get; set; }

        public IEnumerable<BikeViewModel> UserBikes { get; set; }

        public string RaceTrackId { get; set; }

        public int RaceBikeId { get; set; }
    }
}
