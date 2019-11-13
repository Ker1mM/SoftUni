using System;
using System.Collections.Generic;
using System.Text;

namespace PetClinic.DataProcessor.DTO.Export
{
    public class ExportAnimalDTO
    {
        //        {
        //  "OwnerName": "Ivan Ivanov",
        //  "AnimalName": "Jessy",
        //  "Age": 3,
        //  "SerialNumber": "jessiii355",
        //  "RegisteredOn": "05-11-2015"
        //},


        public string OwnerName { get; set; }
        public string AnimalName { get; set; }
        public int Age { get; set; }
        public string SerialNumber { get; set; }
        public string RegisteredOn { get; set; }
    }
}
