using AutoMapper;
using Rider.Domain.Models;
using Rider.Web.Areas.Admin.Models;
using Rider.Web.Areas.Cved.Models;
using Rider.Web.Models.BikeModels;
using Rider.Web.Models.PartModels;
using Rider.Web.Models.PlayerModels;
using Rider.Web.Models.RaceModels;
using Rider.Web.Models.StoreModels;
using System.Linq;

namespace Rider.Services.Mapping
{
    public class RiderProfile : Profile
    {
        public RiderProfile()
        {
            this.CreateMap<Player, PlayerAllViewModel>()
                .ForMember(x => x.CreatedOn, y => y.MapFrom(src => src.CreatedOn.ToString("dd-MM-yyyy HH:mm")));

            this.CreateMap<Part, PartViewModel>();
            this.CreateMap<PartCreateModel, Part>();
            this.CreateMap<Part, PartCreateModel>();
            this.CreateMap<Track, TrackCreateModel>();

            this.CreateMap<Track, TrackViewModel>()
                .ForMember(x => x.AttemptCount, y => y.MapFrom(src => src.Attempts.Count));


            this.CreateMap<TrackCreateModel, Track>();

            this.CreateMap<Bike, BikeViewModel>()
                .ForMember(x => x.SuspensionRating, y => y.MapFrom(src => src.BikeParts.Sum(x => x.PlayerPart.Part.SuspensionRating)))
                .ForMember(x => x.SpeedRating, y => y.MapFrom(src => src.BikeParts.Sum(x => x.PlayerPart.Part.SpeedRating)))
                .ForMember(x => x.Weight, y => y.MapFrom(src => src.BikeParts.Sum(x => x.PlayerPart.Part.Weight)));

            this.CreateMap<BikeCreateModel, Bike>();

            this.CreateMap<Ware, WareViewModel>()
                .ForMember(x => x.PlayerPartId, y => y.MapFrom(src => src.PlayerPartId))
                .ForMember(x => x.Part, y => y.MapFrom(src => src.PlayerPart.Part))
                .ForMember(x => x.SellerName, y => y.MapFrom(src => src.PlayerPart.Player.UserName));

            this.CreateMap<Part, SellPlayerPartModel>()
                .ForMember(x => x.PartId, y => y.MapFrom(src => src.Id));

            this.CreateMap<PlayerParts, PlayerPartViewModel>()
                .ForMember(x => x.PlayerPartId, y => y.MapFrom(src => src.Id));

            this.CreateMap<BikeParts, BikePartViewModel>()
                .ForMember(x => x.BikePartId, y => y.MapFrom(src => src.Id))
                .ForMember(x => x.Part, y => y.MapFrom(src => src.PlayerPart.Part));

            this.CreateMap<Attempt, AttemptViewModel>()
                .ForMember(x => x.PlayerUsername, y => y.MapFrom(src => src.Player.UserName))
                .ForMember(x => x.TrackName, y => y.MapFrom(src => src.Track.Name));

            this.CreateMap<Player, PlayerViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(src => src.UserName))
                 .ForMember(x => x.RegisteredOn, y => y.MapFrom(src => src.CreatedOn));

            this.CreateMap<Player, ProfileViewModel>();
        }
    }
}
