using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Cved.Models
{
    public class TrackViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int Elevation { get; set; } //in meters

        public double Distance { get; set; } //in kilometers

        public TrackType Type { get; set; }

        public int AttemptCount { get; set; }
    }
}
