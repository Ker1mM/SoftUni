using Rider.Web.Areas.Cved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.BikeModels
{
    public class BikePartViewModel
    {
        public PartViewModel Part { get; set; }

        public int BikePartId { get; set; }
    }
}
