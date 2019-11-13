using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.HomeModels
{
    public class StatsViewModel
    {
        public int PlayerCount { get; set; }

        public int TrackCount { get; set; }

        public int PartCount { get; set; }

        public int AttemptCount { get; set; }
    }
}
