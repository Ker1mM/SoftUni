using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;
using VaporStore.Data.Enums;

namespace VaporStore.DataProcessor.Dto.Import
{
    [XmlType("Purchase")]
    public class ImportPurchasesDTO
    {
        [XmlAttribute("title")]
        [Required]
        public string Title { get; set; }

        [XmlElement("Type")]
        [Required]
        public PurchaseType Type { get; set; }

        [Required]
        [RegularExpression("^([0-9A-Z]{4})(-)([0-9A-Z]{4})(-)([0-9A-Z]{4})$")]
        [XmlElement("Key")]
        public string Key { get; set; }

        [Required]
        [RegularExpression("^([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})( +)([0-9]{4})$")]
        [XmlElement("Card")]
        public string Card { get; set; }

        [XmlElement("Date")]
        [Required]
        public string Date { get; set; }
    }
}
