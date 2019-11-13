using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rider.Services.Contracts;
using Rider.Web.Areas.Cved.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Cved.Components
{
    public class TrackMenuViewComponent : ViewComponent
    {
        private readonly ITracksService tracksService;
        private readonly IMapper mapper;

        public TrackMenuViewComponent(ITracksService tracksService, IMapper mapper)
        {
            this.tracksService = tracksService;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var tracks = this.tracksService.GetAllTracks();
            var model = tracks
                .Select(x => this.mapper.Map<TrackViewModel>(x))
                .ToList();

            return View("Menu", model);
        }
    }
}
