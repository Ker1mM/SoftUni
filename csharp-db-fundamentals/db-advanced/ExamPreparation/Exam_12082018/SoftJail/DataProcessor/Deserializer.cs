namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.Data.Models;
    using SoftJail.Data.Models.Enums;
    using SoftJail.DataProcessor.ImportDto;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;

    public static class Deserializer
    {
        public static string ImportDepartmentsCells(SoftJailDbContext context, string jsonString)
        {
            var departmentsCellsDTO = JsonConvert.DeserializeObject<List<ImportDepartmentDTO>>(jsonString);
            var sb = new StringBuilder();

            var departments = new List<Department>();
            var cells = new List<Cell>();

            foreach (var departmentDTO in departmentsCellsDTO)
            {
                if (!IsValid(departmentDTO) || !departmentDTO.Cells.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var department = departments.FirstOrDefault(x => x.Name == departmentDTO.Name);
                if (department == null)
                {
                    department = new Department
                    {
                        Name = departmentDTO.Name
                    };
                }

                var currentDeparmentCells = new List<Cell>();
                foreach (var cellDTO in departmentDTO.Cells)
                {
                    var cell = cells.FirstOrDefault(c => c.CellNumber == cellDTO.CellNumber);
                    if (cell == null)
                    {
                        cell = new Cell
                        {
                            CellNumber = cellDTO.CellNumber,
                            HasWindow = cellDTO.HasWindow

                        };
                        cells.Add(cell);
                    }
                    currentDeparmentCells.Add(cell);
                }

                sb.AppendLine($"Imported {departmentDTO.Name} with {currentDeparmentCells.Count} cells");
                department.Cells = currentDeparmentCells;
                departments.Add(department);
            }

            context.Departments.AddRange(departments);
            context.Cells.AddRange(cells);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportPrisonersMails(SoftJailDbContext context, string jsonString)
        {

            var prisonersMailsDTO = JsonConvert.DeserializeObject<ImportPrisonerMailsDTO[]>(jsonString);

            var sb = new StringBuilder();
            var prisoners = new List<Prisoner>();
            var mails = new List<Mail>();

            foreach (var prisonerMailsDTO in prisonersMailsDTO)
            {
                if (!IsValid(prisonerMailsDTO) || !prisonerMailsDTO.Mails.All(IsValid))
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var prisoner = new Prisoner
                {
                    FullName = prisonerMailsDTO.FullName,
                    Nickname = prisonerMailsDTO.Nickname,
                    Age = prisonerMailsDTO.Age,
                    IncarcerationDate = DateTime.ParseExact(prisonerMailsDTO.IncarcerationDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    ReleaseDate = GetNullableDate(prisonerMailsDTO.ReleaseDate),
                    Bail = prisonerMailsDTO.Bail,
                    CellId = prisonerMailsDTO.CellId
                };

                foreach (var mailDTO in prisonerMailsDTO.Mails)
                {
                    var mail = new Mail
                    {
                        Description = mailDTO.Description,
                        Address = mailDTO.Address,
                        Sender = mailDTO.Sender
                    };

                    mails.Add(mail);
                    prisoner.Mails.Add(mail);
                }

                sb.AppendLine($"Imported {prisoner.FullName} {prisoner.Age} years old");
                prisoners.Add(prisoner);
            }

            context.Prisoners.AddRange(prisoners);
            context.Mails.AddRange(mails);
            context.SaveChanges();

            var result = sb.ToString().TrimEnd();
            return result;
        }

        private static DateTime? GetNullableDate(string date)
        {
            if (date != null)
            {
                return DateTime.ParseExact(date, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            return null;
        }

        public static string ImportOfficersPrisoners(SoftJailDbContext context, string xmlString)
        {
            var sb = new StringBuilder();

            var serializer = new XmlSerializer(typeof(ImportOfficerPrisonerDTO[]), new XmlRootAttribute("Officers"));

            var deserialized = (ImportOfficerPrisonerDTO[])serializer.Deserialize(new StringReader(xmlString));

            var prisonerIds = context.Prisoners.Select(p => p.Id).ToHashSet();
            var officers = new List<Officer>();
            var officersPrisoners = new List<OfficerPrisoner>();

            foreach (var officerDTO in deserialized)
            {
                bool isValidWeapon = Enum.TryParse(typeof(Weapon), officerDTO.Weapon, out object tempWeapon);
                bool isValidPosition = Enum.TryParse(typeof(Position), officerDTO.Position, out object tempPosition);

                if (!IsValid(officerDTO) || !isValidPosition || !isValidWeapon)
                {
                    sb.AppendLine("Invalid Data");
                    continue;
                }

                var officer = new Officer
                {
                    FullName = officerDTO.FullName,
                    Salary = officerDTO.Salary,
                    Position = (Position)tempPosition,
                    Weapon = (Weapon)tempWeapon,
                    DepartmentId = officerDTO.DepartmentId
                };

                foreach (var prisonerDTO in officerDTO.Prisoners)
                {
                    var officerPrisoner = new OfficerPrisoner
                    {
                        Officer = officer,
                        PrisonerId = prisonerDTO.Id
                    };
                    officersPrisoners.Add(officerPrisoner);
                }

                sb.AppendLine($"Imported {officer.FullName} ({officerDTO.Prisoners.Count} prisoners)");
                officers.Add(officer);
            }

            context.Officers.AddRange(officers);
            context.OfficersPrisoners.AddRange(officersPrisoners);
            context.SaveChanges();

            var result = sb.ToString();
            return result.TrimEnd();
        }


        private static bool IsValid(this object obj)
        {
            var validationContext = new ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}