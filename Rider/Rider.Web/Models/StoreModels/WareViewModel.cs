
using Rider.Web.Areas.Cved.Models;

namespace Rider.Web.Models.StoreModels
{
    public class WareViewModel
    {
        public int Id { get; set; }

        public int PlayerPartId { get; set; }

        public PartViewModel Part { get; set; }

        public string SellerName { get; set; }

        public decimal Price { get; set; }
    }
}
