using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Rider.Services.Contracts;
using Rider.Web.Models.StoreModels;
using System.Linq;

namespace Rider.Web.Controllers
{
    [Authorize]
    public class StoreController : Controller
    {
        private readonly IWaresService waresService;
        private readonly IPlayersService playersService;
        private readonly IPartsService partsService;
        private readonly IMapper mapper;

        public StoreController(IWaresService waresService, IPlayersService playersService, IPartsService partsService, IMapper mapper)
        {
            this.waresService = waresService;
            this.playersService = playersService;
            this.partsService = partsService;
            this.mapper = mapper;
        }

        public ActionResult Index()
        {
            var wares = this.waresService.GetAllWares();

            var parts = wares.Select(x => this.mapper.Map<WareViewModel>(x)).ToList();
            var model = new StorePartsViewModel();
            model.Parts = parts;

            return View(model);
        }

        public ActionResult Remove(int w)
        {
            var ware = this.waresService.GetWareById(w);
            if (ware != null)
            {
                bool isAllowedToRemove = this.User.IsInRole("Admin") ||
                    this.User.IsInRole("Moderator") ||
                    ware.PlayerPart.Player.UserName == this.User.Identity.Name;

                if (!isAllowedToRemove)
                {
                    this.TempData["Error"] = "Access denied!";
                    return RedirectToAction(nameof(Index));
                }

                var partId = ware.PlayerPart.PartId;
                var isRemoved = this.waresService.RemoveWareById(w);

                if (isRemoved)
                {
                    this.playersService.RemovePartFromSale(ware.PlayerPartId);
                }
            }

            return Redirect(nameof(Index));
        }

        public ActionResult Buy(int w)
        {
            var username = this.User.Identity.Name;
            var player = this.playersService.GetUserByUsername(username);
            var ware = this.waresService.GetWareById(w);

            if (ware == null)
            {
                this.TempData["Error"] = "Part not found!";
                return RedirectToAction(nameof(Index));
            }

            if (player.Balance < ware.Price)
            {
                this.TempData["Error"] = "Insufficient funds!";
                return RedirectToAction(nameof(Index));
            }

            var officialStoreId = this.playersService.GetUserByUsername("OfficialStore").Id;
            var sellerId = ware.PlayerPart.PlayerId;

            if (sellerId != officialStoreId)
            {
                this.playersService.RemovePartFromInventory(ware.PlayerPart.Id);
                this.playersService.AddTokens(sellerId, ware.Price);
                this.waresService.RemoveWareById(ware.Id);
            }

            this.playersService.RemoveTokens(player.Id, ware.Price);
            this.playersService.AddPartToInventory(ware.PlayerPart.PartId, player.Id);

            return RedirectToAction("Inventory", "Player");
        }
    }
}