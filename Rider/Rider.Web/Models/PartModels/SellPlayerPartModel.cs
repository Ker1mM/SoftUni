using System.ComponentModel.DataAnnotations;

namespace Rider.Web.Models.PartModels
{
    public class SellPlayerPartModel
    {

        public int PartId { get; set; }

        public string Make { get; set; }

        public decimal? BasePrice { get; set; }

        public string Model { get; set; }

        [Required(ErrorMessage = "{0} field is required!")]
        [Range(typeof(decimal), "0", "25142643375935439503", ErrorMessage = "Invalid price!")]
        [DataType(DataType.Currency, ErrorMessage = "Invalid value!")]
        public decimal? Price { get; set; }
    }
}
