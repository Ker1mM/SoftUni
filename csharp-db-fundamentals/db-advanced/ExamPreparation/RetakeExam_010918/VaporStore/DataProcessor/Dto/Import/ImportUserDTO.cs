using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using VaporStore.Data.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    public class ImportUserDTO
    {
        [Required]
        [RegularExpression("^([A-Z]{1}[a-z]+)( )([A-Z]{1}[a-z]+)$")]
        public string FullName { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Range(typeof(int), "3", "103")]
        public int Age { get; set; }

        [Required]
        [MinLength(1)]
        public List<CardDTO> Cards { get; set; }
    }

    public class CardDTO
    {
        [Required]
        [RegularExpression("^([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})$")]
        public string Number { get; set; }

        [Required]
        [RegularExpression("^([0-9]{3})$")]
        public string CVC { get; set; }

        [Required]
        public CardType Type { get; set; }
    }
}
