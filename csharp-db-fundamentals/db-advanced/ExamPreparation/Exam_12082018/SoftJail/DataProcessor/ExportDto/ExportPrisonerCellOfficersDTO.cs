using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoftJail.DataProcessor.ExportDto
{
    public class ExportPrisonerCellOfficersDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int CellNumber { get; set; }

        public List<ExportOfficerDTO> Officers { get; set; }

        public decimal TotalOfficerSalary { get; set; } //Try with string formatting
    }

    public class ExportOfficerDTO
    {
        public string OfficerName { get; set; }

        public string Department { get; set; }
    }
}
