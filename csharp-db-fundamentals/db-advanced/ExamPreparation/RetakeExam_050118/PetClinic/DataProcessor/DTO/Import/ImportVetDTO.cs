using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml.Serialization;

namespace PetClinic.DataProcessor.DTO.Import
{
    [XmlType("Vet")]
    public class ImportVetDTO
    {
        //    <Vets>
        //<Vet>
        //    <Name>Michael Jordan</Name>
        //    <Profession>Emergency and Critical Care</Profession>
        //    <Age>45</Age>
        //    <PhoneNumber>0897665544</PhoneNumber>
        //</Vet>


        [Required]
        [MinLength(3), MaxLength(40)]
        public string Name { get; set; }

        [Required]
        [MinLength(3), MaxLength(50)]
        public string Profession { get; set; }

        [Range(22, 65)]
        public int Age { get; set; }

        [Required]
        [RegularExpression(@"^((\+359[0-9]{9})|(0[0-9]{9}))$")]
        public string PhoneNumber { get; set; }
    }
}
