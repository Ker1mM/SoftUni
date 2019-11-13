using Rider.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.BikeModels
{
    public class BikeViewModel
    {
        public int Id { get; set; }

        public int BikePartsId { get; set; }

        public string Nickname { get; set; }

        public BikeType Type { get; set; }

        public int SuspensionRating { get; set; }

        public int SpeedRating { get; set; }

        public int Weight { get; set; }
    }
}
