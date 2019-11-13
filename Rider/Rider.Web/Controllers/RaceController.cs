using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using Rider.Services.Race;
using Rider.Web.Areas.Cved.Models;
using Rider.Web.Models.BikeModels;
using Rider.Web.Models.RaceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rider.Web.Controllers
{
    [Authorize]
    public class RaceController : Controller
    {
        private readonly ITracksService tracksService;
        private readonly IPlayersService playersService;
        private readonly IBikesService bikesService;
        private readonly IAttemptsService attemptsService;
        private readonly IMapper mapper;

        public RaceController(ITracksService tracksService, IPlayersService playersService, IBikesService bikesService, IAttemptsService attemptsService, IMapper mapper)
        {
            this.tracksService = tracksService;
            this.playersService = playersService;
            this.bikesService = bikesService;
            this.attemptsService = attemptsService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var tracks = this.tracksService.GetAllTracks();
            var model = new RaceIndexViewModel()
            {
                Tracks = tracks
                .Select(x => this.mapper.Map<TrackViewModel>(x))
                .ToList()
            };

            return View(model);
        }

        public ActionResult Track(string t = "")
        {
            var track = this.tracksService.GetTrackById(t);

            if (track == null)
            {
                this.TempData["Error"] = "Track not found!";
                return RedirectToAction(nameof(Index));
            }

            var attempts = this.attemptsService.GetAllAttemptsByTrackId(t);

            var trackModel = this.mapper.Map<TrackViewModel>(track);
            var attemptModels = attempts
                .Select(x => this.mapper.Map<AttemptViewModel>(x))
                .ToList();

            var ownedBikeModels = this.bikesService
                .GetUserBikesByUsername(this.User.Identity.Name)
                .Select(x => this.mapper.Map<BikeViewModel>(x))
                .ToList(); ;

            var model = new TrackDetailsModel()
            {
                Track = trackModel,
                Attempts = attemptModels,
                UserBikes = ownedBikeModels,
            };

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Track(TrackDetailsModel inputModel)
        {
            var raceTrack = this.tracksService.GetTrackById(inputModel.RaceTrackId);
            var raceBike = this.bikesService.GetBikeById(inputModel.RaceBikeId);
            var username = this.User.Identity.Name;

            if (raceTrack == null)
            {
                this.TempData["Error"] = "Invalid track!";
                return RedirectToAction(nameof(Index));
            }

            if (raceBike == null || raceBike.Player.UserName != username)
            {
                this.TempData["Error"] = "Invalid bike!";
                return RedirectToAction(nameof(Index));
            }

            var raceControl = new RaceLogic(raceBike, raceTrack);
            var result = raceControl.GetTime();

            var attempt = new Attempt()
            {
                Player = raceBike.Player,
                PlayerBike = raceBike,
                Track = raceTrack,
                Time = result,
                AttemptedOn = DateTime.UtcNow,
            };

            this.attemptsService.AddAttempt(attempt);

            return RedirectToAction(nameof(Track), new { t = inputModel.RaceTrackId });
        }
    }
}