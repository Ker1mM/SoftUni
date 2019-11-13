using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Rider.Domain.Models;
using Rider.Services.Contracts;
using Rider.Web.Areas.Admin.Data;
using Rider.Web.Areas.Admin.Models;
using Rider.Web.Data.Enums;

namespace Rider.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class PlayerController : Controller
    {
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;

        public PlayerController(IPlayersService playersService, IMapper mapper)
        {
            this.playersService = playersService;
            this.mapper = mapper;
        }

        public ActionResult All()
        {
            return ViewComponent(typeof(PlayerAllComponent));
        }

        public ActionResult Edit(string username = "", string role = "")
        {
            var player = this.playersService.GetUserByUsername(username);
            var roleExist = Enum.TryParse<Role>(role, out Role resultRole);

            if (player == null)
            {
                this.TempData["Error"] = "Player not found!";
                return RedirectToAction(nameof(All));
            }

            if (!roleExist)
            {
                this.TempData["Error"] = "Role is invalid!";
                return RedirectToAction(nameof(All));
            }

            var model = new PlayerEditModel
            {
                Username = username,
                Role = role
            };

            return this.View(nameof(this.Edit), model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PlayerEditModel inputModel)
        {
            if (!ModelState.IsValid)
            {
                return this.View(inputModel);
            }

            this.playersService.UpdateRoleByUsername(inputModel.Username, inputModel.Role, inputModel.NewRole);

            var player = this.playersService.GetUserByUsername(inputModel.Username);
            var newRoles = this.playersService.GetUserRoles(player.UserName);
            return RedirectToAction("Edit", new { Username = inputModel.Username, Role = newRoles });
        }
    }
}