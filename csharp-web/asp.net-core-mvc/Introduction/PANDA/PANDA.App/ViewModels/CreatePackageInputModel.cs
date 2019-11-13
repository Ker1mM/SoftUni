using PANDA.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PANDA.App.ViewModels
{
    public class CreatePackageInputModel
    {
        public RecipientViewModel Recipients { get; set; }

        [Required]
        public string Description { get; set; }

        public decimal Weight { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public PandaUser Recipient { get; set; }
    }
}
