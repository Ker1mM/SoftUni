using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace VaporStore.DataProcessor.Dto.Export
{
    [XmlType("User")]
    public class UserPurchasesDTO
    {
        [XmlAttribute("username")]
        public string Username { get; set; }

        [XmlArray("Purchases")]
        public List<PurchaseDTO> Purchases { get; set; }

        public decimal TotalSpent { get; set; }
    }

    [XmlType("Purchase")]
    public class PurchaseDTO
    {
        public string Card { get; set; }

        public string Cvc { get; set; }


        [XmlIgnore]
        public DateTime Date { get; set; }

        [XmlElement("Date")]
        public string DateFormatted
        {
            get { return this.Date.ToString("yyyy-MM-dd HH:mm"); }
            set { this.Date = DateTime.Parse(value); }
        }

        public GameDTO Game { get; set; }

        [XmlIgnore]
        public string Username { get; set; }
    }

    public class GameDTO
    {
        [XmlAttribute("title")]
        public string Title { get; set; }

        public string Genre { get; set; }

        public decimal Price { get; set; }
    }
}
