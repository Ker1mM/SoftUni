using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rider.Domain.Enums;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using Rider.Web.Models.BikeModels;
using Rider.Web.Models.PartModels;
using System.Linq;

namespace Rider.Web.Controllers
{
    [Authorize]
    public class BikesController : Controller
    {
        private readonly IAttemptsService attemptsService;
        private readonly IPlayersService playersService;
        private readonly IBikesService bikesService;
        private readonly IPartsService partsService;
        private readonly IMapper mapper;

        public BikesController(IAttemptsService attemptsService, IPlayersService playersService, IBikesService bikesService, IPartsService partsService, IMapper mapper)
        {
            this.attemptsService = attemptsService;
            this.playersService = playersService;
            this.bikesService = bikesService;
            this.partsService = partsService;
            this.mapper = mapper;
        }

        public ActionResult Index(BikeAllViewModel model)
        {
            var username = this.User.Identity.Name;

            var bikes = this.bikesService
                .GetUserBikesByUsername(username)
                .Select(x => this.mapper.Map<BikeViewModel>(x))
                .ToList();

            model.Bikes = bikes;

            return View(model);
        }

        public ActionResult Add()
        {
            var username = this.User.Identity.Name;
            var playerBikeCount = this.bikesService.GetUserBikesByUsername(username).Count();

            bool canCreateMoreBikes =
                playerBikeCount < 3 ||
                (playerBikeCount < 5 && !this.User.IsInRole("User"));

            if (!canCreateMoreBikes)
            {
                this.TempData["Error"] = "Bike limit reached!";

                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Add(BikeCreateModel model)
        {
            var username = this.User.Identity.Name;

            if (this.bikesService.NicknameTaken(model.Nickname, username))
            {
                ModelState.AddModelError("Nickname", "Nickname has been taken!");
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var bike = this.mapper.Map<Bike>(model);

            this.bikesService.AddBikeToPlayer(username, bike);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult Equip(int b = 0)
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetPlayerByUsername(username);

            var model = new EquipPartModel();

            if (!player.Bikes.Any(x => x.Id == b))
            {
                this.TempData["Error"] = "Bike does not exist!";

                return RedirectToAction(nameof(Index));
            }

            var bike = this.bikesService.GetBikeById(b);
            var bikeModel = this.mapper.Map<BikeViewModel>(bike);

            var bikePartModels = bike.BikeParts
                .Select(x => this.mapper.Map<BikePartViewModel>(x))
                .ToList();

            model.Parts = bikePartModels;
            model.Bike = bikeModel;

            var partModels = player.Inventory.Where(x => x.IsForSale == false && x.IsUsed == false)
                .Select(x => this.mapper.Map<PlayerPartViewModel>(x))
                .ToList();

            model.AvailableParts = partModels;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Equip(EquipPartModel model)
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetPlayerByUsername(username);

            if (!player.Bikes.Any(x => x.Id == model.BikeId))
            {
                this.ModelState.AddModelError("", "Bike not found!");
            }

            if (model.NewPlayerPartId == 0)
            {
                TempData["Error"] = "No part was selected!";
                return RedirectToAction(nameof(Equip), new { b = model.BikeId });
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var newPlayerPart = this.playersService.GetPlayerPartById(model.NewPlayerPartId);
            var bike = this.bikesService.GetBikeById(model.BikeId);
            var bikeParts = bike.BikeParts.Where(x => x.PlayerPart.Part.Type == newPlayerPart.Part.Type);

            if ((newPlayerPart.Part.Type != PartType.Other && bikeParts.Count() >= 1) || bikeParts.Count() >= 3)
            {
                var oldBikePart = bikeParts.FirstOrDefault();
                this.playersService.DisusePart(oldBikePart.PlayerPartId);
                this.bikesService.RemoveBikePart(oldBikePart.Id);
            }

            var newBikePart = new BikeParts();

            newBikePart.PlayerPart = newPlayerPart;
            newBikePart.Bike = bike;

            var addedBikePart = this.bikesService.AddBikePart(newBikePart);
            this.playersService.UsePart(model.NewPlayerPartId, addedBikePart.Id);

            return RedirectToAction(nameof(Equip), new { b = model.BikeId });
        }

        public ActionResult Remove(int b = 0, int p = 0)
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetPlayerByUsername(username);

            if (player.Bikes.Any(x => x.Id == b))
            {
                var bikePart = this.bikesService.GetBikePartById(p);

                if (bikePart != null)
                {
                    this.bikesService.RemoveBikePart(p);
                    this.playersService.DisusePart(bikePart.PlayerPartId);
                }
            }

            return RedirectToAction(nameof(Equip), new { b });
        }

        public ActionResult Delete(int b)
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetPlayerByUsername(username);

            if (!player.Bikes.Any(x => x.Id == b))
            {
                this.TempData["Error"] = "Invalid bike!";
                return RedirectToAction(nameof(Index));
            }

            var bike = this.bikesService.GetBikeById(b);

            if (bike.BikeParts.Count > 0)
            {
                this.TempData["Error"] = "You have to remove the parts of the bike to delete it!";
                return RedirectToAction(nameof(Equip), new { b });
            }

            this.attemptsService.RemoveAllAtemptsByBikeId(b);
            this.bikesService.RemoveBikeById(b);

            return RedirectToAction(nameof(Index));
        }
    }
}