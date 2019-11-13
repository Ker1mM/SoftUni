using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportGamesDTO
    {
        [Required]
        public string Name { get; set; }

        [Range(0.0, double.MaxValue)]
        public decimal Price { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Developer { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Genre { get; set; }

        [Required]
        [MinLength(1)]
        public List<string> Tags { get; set; }
    }
}
