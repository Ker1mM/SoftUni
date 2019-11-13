namespace PetClinic.App
{
    using AutoMapper;
    using PetClinic.DataProcessor.DTO.Import;
    using PetClinic.Models;
    using System;
    using System.Globalization;

    public class PetClinicProfile : Profile
    {
        // Configure your AutoMapper here if you wish to use it. If not, DO NOT DELETE THIS CLASS
        public PetClinicProfile()
        {
            this.CreateMap<ImportAnimalAidDTO, AnimalAid>();
            this.CreateMap<ImportAnimalDTO, Animal>();
            this.CreateMap<ImportPassportDTO, Passport>()
                .ForMember(p => p.RegistrationDate, opt => opt.MapFrom(src => DateTime.ParseExact(src.RegistrationDate, "dd-MM-yyyy", CultureInfo.InvariantCulture)));

            this.CreateMap<ImportVetDTO, Vet>();
        }
    }
}
