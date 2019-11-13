using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rider.Services.Contracts;
using Rider.Web.Areas.Cved.Models;
using Rider.Web.Models;
using Rider.Web.Models.HomeModels;
using Rider.Web.Models.PlayerModels;
using Rider.Web.Models.RaceModels;
using Rider.Web.Models.StoreModels;
using System.Diagnostics;
using System.Linq;

namespace Rider.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IPartsService partsService;
        private readonly ITracksService tracksService;
        private readonly IWaresService waresService;
        private readonly IPlayersService playersService;
        private readonly IAttemptsService attemptsService;
        private readonly IMapper mapper;

        public HomeController(IPartsService partsService, ITracksService tracksService, IWaresService waresService, IPlayersService playersService, IAttemptsService attemptsService, IMapper mapper)
        {
            this.partsService = partsService;
            this.tracksService = tracksService;
            this.waresService = waresService;
            this.playersService = playersService;
            this.attemptsService = attemptsService;
            this.mapper = mapper;
        }

        [AllowAnonymous]
        public ActionResult Index()
        {
            var ware = this.waresService.GetLatestWare();
            var player = this.playersService.GetLatestPlayer();
            var attempt = this.attemptsService.GetLatestAttempt();
            var track = this.tracksService.GetMostPopular();

            var wareModel = ware == null ? null : this.mapper.Map<WareViewModel>(ware);
            var attemptModel = attempt == null ? null : this.mapper.Map<AttemptViewModel>(attempt);
            var trackModel = track == null ? null : this.mapper.Map<TrackViewModel>(track);

            var model = new IndexViewModel()
            {
                Ware = wareModel,
                Player = this.mapper.Map<PlayerViewModel>(player),
                Attempt = attemptModel,
                Track = trackModel,
            };

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult How()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Error404()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult Stats()
        {
            var model = new StatsViewModel()
            {
                PlayerCount = this.playersService.GetAllPlayers().Count(),
                TrackCount = this.tracksService.GetAllTracks().Count(),
                PartCount = this.partsService.GetAllParts().Count(),
                AttemptCount = this.attemptsService.GetAllAttemptsCount(),
            };

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
