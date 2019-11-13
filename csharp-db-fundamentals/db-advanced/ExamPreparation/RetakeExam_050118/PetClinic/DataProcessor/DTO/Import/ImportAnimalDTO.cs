using PetClinic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PetClinic.DataProcessor.DTO.Import
{
    public class ImportAnimalDTO
    {
        //        "Name":"Bella",
        //"Type":"cat",
        //"Age": 2,
        //"Passport": {
        //  "SerialNumber": "etyhGgH678",
        //  "OwnerName": "Sheldon Cooper",
        //  "OwnerPhoneNumber": "0897556446",
        //  "RegistrationDate": "12-03-2014"

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [MinLength(3), MaxLength(20)]
        public string Type { get; set; }

        [Range(1, int.MaxValue)]
        public int Age { get; set; }

        public ImportPassportDTO Passport { get; set; }

    }

    public class ImportPassportDTO
    {
        [Key]
        [RegularExpression("^([a-zA-Z]{7}[0-9]{3})$")]
        public string SerialNumber { get; set; }

        [Required]
        [RegularExpression(@"^((\+359[0-9]{9})|(0[0-9]{9}))$")]
        public string OwnerPhoneNumber { get; set; }

        [Required]
        [MinLength(3), MaxLength(30)]
        public string OwnerName { get; set; }

        public string RegistrationDate { get; set; }
    }
}
