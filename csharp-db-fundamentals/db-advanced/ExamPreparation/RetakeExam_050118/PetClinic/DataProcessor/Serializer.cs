namespace PetClinic.DataProcessor
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml;
    using System.Xml.Serialization;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.DTO.Export;

    public class Serializer
    {
        public static string ExportAnimalsByOwnerPhoneNumber(PetClinicContext context, string phoneNumber)
        {
            var animals = context.Animals
                 .Where(a => a.Passport.OwnerPhoneNumber == phoneNumber)
                 .Select(x => new ExportAnimalDTO
                 {
                     OwnerName = x.Passport.OwnerName,
                     AnimalName = x.Name,
                     Age = x.Age,
                     SerialNumber = x.PassportSerialNumber,
                     RegisteredOn = x.Passport.RegistrationDate.ToString("dd-MM-yyyy", CultureInfo.InvariantCulture)
                 })
                 .OrderBy(x => x.Age)
                 .ThenBy(x => x.SerialNumber)
                 .ToArray();

            var result = JsonConvert.SerializeObject(animals, Newtonsoft.Json.Formatting.Indented);
            return result;
        }

        public static string ExportAllProcedures(PetClinicContext context)
        {
            var serializer = new XmlSerializer(typeof(ExportProcedureDTO[]), new XmlRootAttribute("Procedures"));

            var procedures = context.Procedures
                .Select(a => new ExportProcedureDTO
                {
                    Passport = a.Animal.PassportSerialNumber,
                    OwnerNumber = a.Animal.Passport.OwnerPhoneNumber,
                    DateTime = a.DateTime,
                    AnimalAids = a.ProcedureAnimalAids
                    .Select(p => new ExportAnimalAidDTO
                    {
                        Name = p.AnimalAid.Name,
                        Price = p.AnimalAid.Price
                    })
                    .ToList(),
                    TotalPrice = a.ProcedureAnimalAids.Sum(p => p.AnimalAid.Price)
                })
                .OrderBy(x => x.DateTime)
                .ThenBy(x => x.Passport)
                .ToArray();


            var sb = new StringBuilder();

            var namespaces = new XmlSerializerNamespaces(new[]
            {
                XmlQualifiedName.Empty
            });

            serializer.Serialize(new StringWriter(sb), procedures, namespaces);

            return sb.ToString();
        }
    }
}
