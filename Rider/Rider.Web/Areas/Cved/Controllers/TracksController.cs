using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using Rider.Web.Areas.Cved.Components;
using Rider.Web.Areas.Cved.Models;

namespace Rider.Web.Areas.Cved.Controllers
{
    [Area("Cved")]
    [Authorize(Roles = "Admin, Moderator")]
    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;
        private readonly IMapper mapper;

        public TracksController(ITracksService tracksService, IMapper mapper)
        {
            this.tracksService = tracksService;
            this.mapper = mapper;
        }

        public ActionResult Menu()
        {
            return ViewComponent(typeof(TrackMenuViewComponent));
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(TrackCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.View(model);
            }

            var track = this.mapper.Map<Track>(model);
            this.tracksService.AddTrack(track);

            return RedirectToAction("Menu");
        }

        public ActionResult Edit(string trackId)
        {
            var track = this.tracksService.GetTrackById(trackId);

            if (track == null)
            {
                this.TempData["Error"] = "Track not found!";
                return RedirectToAction(nameof(Menu));
            }

            var model = this.mapper.Map<TrackCreateModel>(track);
            model.TrackId = trackId;
            return View(model);

        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Edit(TrackCreateModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var track = this.mapper.Map<Track>(inputModel);
            track.Id = inputModel.TrackId;
            this.tracksService.EditTrack(track);

            return RedirectToAction(nameof(Menu));
        }


        public ActionResult Archive(string t)
        {
            var track = this.tracksService.GetTrackById(t);

            if (track == null)
            {
                this.TempData["Error"] = "Track not found!";
                return RedirectToAction(nameof(Menu));
            }

            track = this.tracksService.ArchiveTrackById(t);

            return RedirectToAction(nameof(Menu));
        }
    }
}