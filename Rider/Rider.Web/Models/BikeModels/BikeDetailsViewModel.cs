using Rider.Web.Areas.Cved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Models.BikeModels
{
    public class BikeDetailsViewModel
    {
        public BikeDetailsViewModel()
        {
        }

        public int AllowedExtraParts { get; set; }

        public BikeViewModel Bike { get; set; }

        public IEnumerable<PartViewModel> Parts { get; set; }
    }
}
