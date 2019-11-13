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
    public class PartsController : Controller
    {
        private readonly IPartsService partsService;
        private readonly IMapper mapper;
        private readonly IPlayersService playersService;
        private readonly IWaresService waresService;

        public PartsController(IPartsService partsService, IMapper mapper, IPlayersService playersService, IWaresService waresService)
        {
            this.partsService = partsService;
            this.mapper = mapper;
            this.playersService = playersService;
            this.waresService = waresService;
        }

        public ActionResult Menu()
        {
            return ViewComponent(typeof(PartMenuViewComponent));
        }


        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Create(PartCreateModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            var part = this.mapper.Map<Part>(inputModel);
            part = this.partsService.AddPart(part);
            var player = this.playersService.GetUserByUsername("OfficialStore");
            this.playersService.AddPartToInventory(part.Id, player.Id);

            return Redirect("Menu");
        }

        public ActionResult Edit(int partId)
        {
            var part = this.partsService.GetPartById(partId);

            if (part == null)
            {
                this.TempData["Error"] = "Part not found!";
                return RedirectToAction(nameof(Menu));
            }

            var viewModel = this.mapper.Map<PartCreateModel>(part);
            viewModel.PartId = partId;

            return View(viewModel);
        }

        public ActionResult Sell(int partId)
        {
            var player = this.playersService.GetPlayerByUsername("OfficialStore");
            var part = this.partsService.GetPartById(partId);

            if (part == null)
            {
                this.TempData["Error"] = "Part not found!";
                return RedirectToAction(nameof(Menu));
            }

            var playerPart = player.Inventory.FirstOrDefault(x => x.PartId == partId);

            if (!playerPart.IsForSale)
            {
                this.playersService.SetPartForSale(playerPart.Id);
                var ware = new Ware()
                {
                    PlayerPart = playerPart,
                    ListedOn = DateTime.UtcNow,
                    Price = part.BasePrice,
                };

                this.waresService.AddWare(ware);
            }

            return RedirectToAction("Index", "Store");
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Edit(PartCreateModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return View(inputModel);
            }

            var part = this.mapper.Map<Part>(inputModel);
            part.Id = (int)inputModel.PartId;
            this.partsService.EditPart(part);

            return RedirectToAction(nameof(Menu));
        }
    }
}