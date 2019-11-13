using Microsoft.AspNetCore.Mvc.Rendering;
using Rider.Web.Models.PartModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Rider.Web.Models.BikeModels
{
    public class EquipPartModel
    {
        public int BikeId { get; set; }

        public BikeViewModel Bike { get; set; }

        public int NewPlayerPartId { get; set; }

        public IEnumerable<BikePartViewModel> Parts { get; set; }

        public IEnumerable<PlayerPartViewModel> AvailableParts { get; set; }
    }
}
