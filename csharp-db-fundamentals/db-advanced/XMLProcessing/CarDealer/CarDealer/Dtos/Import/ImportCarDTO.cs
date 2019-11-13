using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace CarDealer.Dtos.Import
{
    [XmlType("Car")]
    public class ImportCarDTO
    {
        [XmlElement("make")]
        public string Make { get; set; }

        [XmlElement("model")]
        public string Model { get; set; }

        [XmlElement("TraveledDistance")]
        public long TravelledDistance { get; set; }

        [XmlArray("parts")]
        [XmlArrayItem("partId")]
        public List<PartIdDTO> PartIds { get; set; }
    }

    public class PartIdDTO
    {
        [XmlAttribute("id")]
        public int Id { get; set; }
    }
}
