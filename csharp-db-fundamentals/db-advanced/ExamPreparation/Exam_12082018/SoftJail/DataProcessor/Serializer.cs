namespace SoftJail.DataProcessor
{

    using Data;
    using Newtonsoft.Json;
    using SoftJail.DataProcessor.ExportDto;
    using System;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;

    public class Serializer
    {
        public static string ExportPrisonersByCells(SoftJailDbContext context, int[] ids)
        {
            var prisoners = context.Prisoners
                .Where(p => ids.Any(i => i == p.Id))
                .Select(p => new ExportPrisonerCellOfficersDTO
                {
                    Id = p.Id,
                    Name = p.FullName,
                    CellNumber = p.Cell.CellNumber,
                    Officers = p.PrisonerOfficers
                        .Select(po => new ExportOfficerDTO
                        {
                            OfficerName = po.Officer.FullName,
                            Department = po.Officer.Department.Name
                        })
                        .OrderBy(o => o.OfficerName)
                        .ToList(),
                    TotalOfficerSalary = p.PrisonerOfficers.Sum(po => po.Officer.Salary)
                })
                .OrderBy(p => p.Name)
                .ThenBy(p => p.Id)
                .ToList();

            var result = JsonConvert.SerializeObject(prisoners, Newtonsoft.Json.Formatting.Indented);
            return result;
        }

        public static string ExportPrisonersInbox(SoftJailDbContext context, string prisonersNames)
        {
            var att = new XmlRootAttribute("Prisoners");
            var serializer = new XmlSerializer(typeof(ExportPrisonerInboxDTO[]), att);

            var names = prisonersNames.Split(",", StringSplitOptions.RemoveEmptyEntries);

            var prisonersInboxes = context.Prisoners
                  .Where(pi => names.Any(n => n == pi.FullName))
                  .Select(p => new ExportPrisonerInboxDTO
                  {
                      Id = p.Id,
                      Name = p.FullName,
                      IncarcerationDate = p.IncarcerationDate,
                      EncryptedMessages = p.Mails
                         .Select(m => new MessageDTO
                         {
                             Description = GetEncryptedMail(m.Description)
                         })
                         .ToList()
                  })
                  .OrderBy(p => p.Name)
                  .ThenBy(p => p.Id)
                  .ToArray();

            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[] {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), prisonersInboxes, namespaces);
            return sb.ToString();
        }

        private static string GetEncryptedMail(string mail)
        {
            var array = mail.ToCharArray().Reverse();

            return string.Join("", array);
        }
    }
}