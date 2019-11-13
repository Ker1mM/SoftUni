using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using Rider.Web.Models.PartModels;
using Rider.Web.Models.PlayerModels;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Controllers
{
    [Authorize]
    public class PlayerController : Controller
    {
        private readonly IWaresService waresService;
        private readonly IPartsService partsService;
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;

        public PlayerController(IWaresService waresService, IPartsService partsService, IPlayersService playersService, IMapper mapper)
        {
            this.waresService = waresService;
            this.partsService = partsService;
            this.playersService = playersService;
            this.mapper = mapper;
        }

        public ActionResult Profile()
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetUserByUsername(username);

            var model = this.mapper.Map<ProfileViewModel>(player);
            var roles = this.playersService.GetUserRoles(username);
            model.Role = roles;

            return View(model);
        }

        public ActionResult Inventory()
        {
            var username = this.User.Identity.Name;

            var playerParts = this.partsService.GetAllPlayerPartsByUserName(username);

            var model = new InventoryViewModel();

            model.AvailableParts = playerParts
                .Where(x => !x.IsForSale && !x.IsUsed)
                .Select(x => this.mapper.Map<PlayerPartViewModel>(x))
                .ToList();

            model.PartsForSale = playerParts
                .Where(x => x.IsForSale && !x.IsUsed)
                .Select(x => this.mapper.Map<PlayerPartViewModel>(x))
                .ToList();

            model.UsedParts = playerParts
                .Where(x => !x.IsForSale && x.IsUsed)
                .Select(x => this.mapper.Map<PlayerPartViewModel>(x))
                .ToList(); ;

            return View(model);
        }

        public ActionResult Sell(int p = 0)
        {
            var username = this.User.Identity.Name;

            var playerPart = this.partsService.GetPlayerPartByPartId(username, p);

            if (playerPart == null)
            {
                this.TempData["Error"] = "Part not found!";
                return RedirectToAction(nameof(Inventory));
            }

            var model = this.mapper.Map<SellPlayerPartModel>(playerPart.Part);
            model.PartId = p;

            return View(model);
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public ActionResult Sell(SellPlayerPartModel bindModel)
        {


            var username = this.User.Identity.Name;

            var playerPart = this.partsService.GetPlayerPartByPartId(username, bindModel.PartId);

            if (playerPart == null)
            {
                this.TempData["Error"] = "Part not found!";
                return RedirectToAction(nameof(Inventory));
            }

            mapper.Map<Part, SellPlayerPartModel>(playerPart.Part, bindModel);

            if (!ModelState.IsValid)
            {
                return View(bindModel);
            }


            var ware = new Ware()
            {
                PlayerPartId = playerPart.Id,
                ListedOn = DateTime.UtcNow,
                Price = (decimal)bindModel.Price,
            };

            this.waresService.AddWare(ware);
            this.playersService.SetPartForSale(playerPart.Id);

            return RedirectToAction(nameof(Inventory));
        }
    }
}