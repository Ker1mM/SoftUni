using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.DTO.Export
{
    [XmlType("Procedure")]
    public class ExportProcedureDTO
    {
        //      <Procedures>
        //<Procedure>
        //  <Passport>acattee321</Passport>
        //  <OwnerNumber>0887446123</OwnerNumber>
        //  <DateTime>14-01-2016</DateTime>
        //  <AnimalAids>
        //    <AnimalAid>
        //      <Name>Internal Deworming</Name>
        //      <Price>8.00</Price>
        //    </AnimalAid>
        //    <AnimalAid>
        //      <Name>Fecal Test</Name>
        //      <Price>7.50</Price>
        //    </AnimalAid>
        //    <AnimalAid>
        //      <Name>Nasal Bordetella</Name>
        //      <Price>5.60</Price>
        //    </AnimalAid>
        //  </AnimalAids>
        //  <TotalPrice>21.10</TotalPrice>
        //</Procedure>

        public string Passport { get; set; }
        public string OwnerNumber { get; set; }

        [XmlIgnore]
        public DateTime DateTime { get; set; }

        [XmlElement("DateTime")]
        public string DateTimeFormatted
        {
            get => this.DateTime.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture);

            set => this.DateTime = DateTime.Parse(value);
        }

        [XmlArray("AnimalAids")]
        public List<ExportAnimalAidDTO> AnimalAids { get; set; }

        public decimal TotalPrice { get; set; }
    }

    [XmlType("AnimalAid")]
    public class ExportAnimalAidDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
