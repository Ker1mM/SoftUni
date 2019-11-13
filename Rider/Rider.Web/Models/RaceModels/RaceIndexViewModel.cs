using Rider.Web.Areas.Cved.Models;
using System.Collections.Generic;

namespace Rider.Web.Models.RaceModels
{
    public class RaceIndexViewModel
    {
        public IEnumerable<TrackViewModel> Tracks { get; set; }
    }
}
