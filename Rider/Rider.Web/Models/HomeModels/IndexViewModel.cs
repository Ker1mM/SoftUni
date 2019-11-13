using Rider.Web.Areas.Cved.Models;
using Rider.Web.Models.PlayerModels;
using Rider.Web.Models.RaceModels;
using Rider.Web.Models.StoreModels;

namespace Rider.Web.Models.HomeModels
{
    public class IndexViewModel
    {
        public WareViewModel Ware { get; set; }

        public PlayerViewModel Player { get; set; }

        public TrackViewModel Track { get; set; }

        public AttemptViewModel Attempt { get; set; }
    }
}
