using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.ViewModels.Cats
{
    public class AddCatInputModel
    {
        [Required()]
        public string Name { get; set; }

        [Required]
        [Range(0, 50)]
        public int Age { get; set; }

        [Required]
        public string Breed { get; set; }

        [Required]
        public string ImageURL { get; set; }
    }
}
