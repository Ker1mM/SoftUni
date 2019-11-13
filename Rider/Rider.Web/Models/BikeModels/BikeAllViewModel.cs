using System.Collections.Generic;

namespace Rider.Web.Models.BikeModels
{
    public class BikeAllViewModel
    {
        public BikeAllViewModel()
        {
        }

        public BikeAllViewModel(string errorMessage)
        {
            this.ErrorMessage = errorMessage;
        }

        public IEnumerable<BikeViewModel> Bikes { get; set; }

        public string ErrorMessage { get; set; }
    }
}
