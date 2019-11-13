namespace PetClinic.DataProcessor
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Xml.Serialization;
    using AutoMapper;
    using Newtonsoft.Json;
    using PetClinic.Data;
    using PetClinic.DataProcessor.DTO.Import;
    using PetClinic.Models;

    public static class Deserializer
    {

        public static string ImportAnimalAids(PetClinicContext context, string jsonString)
        {
            var animalAidsDTO = JsonConvert.DeserializeObject<ImportAnimalAidDTO[]>(jsonString);

            var animalAids = new List<AnimalAid>();
            var sb = new StringBuilder();

            foreach (var animalAidDTO in animalAidsDTO)
            {
                if (!IsValid(animalAidDTO) || animalAids.Any(x => x.Name == animalAidDTO.Name))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var animalAid = Mapper.Map<AnimalAid>(animalAidDTO);
                animalAids.Add(animalAid);
                sb.AppendLine($"Record {animalAid.Name} successfully imported.");
            }

            context.AnimalAids.AddRange(animalAids);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportAnimals(PetClinicContext context, string jsonString)
        {
            var animalsDTO = JsonConvert.DeserializeObject<ImportAnimalDTO[]>(jsonString);
            var sb = new StringBuilder();

            var animals = new List<Animal>();
            var passports = new List<Passport>();

            foreach (var animalDTO in animalsDTO)
            {
                if (!IsValid(animalDTO) || !IsValid(animalDTO.Passport) || passports.Any(x => x.SerialNumber == animalDTO.Passport.SerialNumber))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var animal = Mapper.Map<Animal>(animalDTO);
                var passport = animal.Passport;

                passports.Add(passport);
                animals.Add(animal);
                sb.AppendLine($"Record {animal.Name} Passport №: {passport.SerialNumber} successfully imported.");
            }

            context.Animals.AddRange(animals);
            context.Passports.AddRange(passports);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        public static string ImportVets(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportVetDTO[]), new XmlRootAttribute("Vets"));

            var vetsDTO = (ImportVetDTO[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var vets = new List<Vet>();

            foreach (var vetDTO in vetsDTO)
            {
                if (!IsValid(vetDTO) || vets.Any(v => v.PhoneNumber == vetDTO.PhoneNumber))
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var vet = Mapper.Map<Vet>(vetDTO);
                vets.Add(vet);

                sb.AppendLine($"Record {vet.Name} successfully imported.");
            }

            context.Vets.AddRange(vets);
            context.SaveChanges();
            return sb.ToString().TrimEnd();
        }

        public static string ImportProcedures(PetClinicContext context, string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ImportProcedureDTO[]), new XmlRootAttribute("Procedures"));

            var proceduresDTO = (ImportProcedureDTO[])serializer.Deserialize(new StringReader(xmlString));

            var sb = new StringBuilder();

            var procedures = new List<Procedure>();

            var vets = context.Vets.ToHashSet();
            var animals = context.Animals.ToHashSet();
            var animalAids = context.AnimalAids.ToHashSet();

            foreach (var procedureDTO in proceduresDTO)
            {
                var vet = vets.FirstOrDefault(x => x.Name == procedureDTO.VetName);
                var animal = animals.FirstOrDefault(x => x.PassportSerialNumber == procedureDTO.AnimalSerialNumber);

                bool animalAidsIsValid = procedureDTO.AnimalAids.Any(x => animalAids.Any(y => x.Name == y.Name));
                bool animalAidsCountIsValid = procedureDTO.AnimalAids.Count == procedureDTO.AnimalAids.Select(x => x.Name).Distinct().Count();

                bool procedureIsValid = (vet != null) && (animal != null) && animalAidsCountIsValid && animalAidsIsValid;

                if (!IsValid(procedureDTO) || !procedureDTO.AnimalAids.All(IsValid) || !procedureIsValid)
                {
                    sb.AppendLine($"Error: Invalid data.");
                    continue;
                }

                var currentProcedureAnimalAids = new List<ProcedureAnimalAid>();
                foreach (var animalAidsDTO in procedureDTO.AnimalAids)
                {
                    var animalAid = animalAids.FirstOrDefault(x => x.Name == animalAidsDTO.Name);
                    var procedureAnimalAid = new ProcedureAnimalAid
                    {
                        AnimalAid = animalAid
                    };

                    currentProcedureAnimalAids.Add(procedureAnimalAid);
                }

                var procedure = new Procedure
                {
                    Animal = animal,
                    Vet = vet,
                    ProcedureAnimalAids = currentProcedureAnimalAids,
                    DateTime = DateTime.ParseExact(procedureDTO.DateTime, "dd-MM-yyyy", CultureInfo.InvariantCulture)
                };

                procedures.Add(procedure);
                sb.AppendLine($"Record successfully imported.");
            }

            context.Procedures.AddRange(procedures);
            context.SaveChanges();

            return sb.ToString().TrimEnd();
        }

        private static bool IsValid(this object obj)
        {
            var validationContext = new System.ComponentModel.DataAnnotations.ValidationContext(obj);
            var validationResults = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

            return isValid;
        }
    }
}
