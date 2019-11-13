using Rider.Web.Models.PartModels;
using System.Collections.Generic;

namespace Rider.Web.Models.PlayerModels
{
    public class InventoryViewModel
    {
        public IEnumerable<PlayerPartViewModel> AvailableParts { get; set; }

        public IEnumerable<PlayerPartViewModel> PartsForSale { get; set; }

        public IEnumerable<PlayerPartViewModel> UsedParts { get; set; }
    }
}
