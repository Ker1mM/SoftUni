using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Cved.Models
{
    public class PartViewModel
    {
        public int Id { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int Weight { get; set; } //in grams

        public int SpeedRating { get; set; }

        public int SuspensionRating { get; set; }

        public PartType Type { get; set; }

        public string Description { get; set; }

        public decimal BasePrice { get; set; }

    }
}
