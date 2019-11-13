using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Rider.Services.Contracts;
using Rider.Web.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rider.Web.Areas.Admin.Data
{
    public class PlayerAllComponent : ViewComponent
    {
        private readonly IPlayersService playersService;
        private readonly IMapper mapper;

        public PlayerAllComponent(IPlayersService playersService, IMapper mapper)
        {
            this.playersService = playersService;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {

            var users = this.playersService.GetAllPlayers().ToList();
            var playerModels = new List<PlayerAllViewModel>();

            foreach (var player in users)
            {
                var currentModel = this.mapper.Map<PlayerAllViewModel>(player);
                currentModel.Role = this.playersService.GetUserRoles(player.UserName);

                playerModels.Add(currentModel);
            }

            var model = new IndexViewModel()
            {
                Players = playerModels,
            };

            return this.View("All", model);
        }
    }
}
