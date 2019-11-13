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
    public class PartMenuViewComponent : ViewComponent
    {
        private readonly IPartsService partsService;
        private readonly IMapper mapper;

        public PartMenuViewComponent(IPartsService partsService, IMapper mapper)
        {
            this.partsService = partsService;
            this.mapper = mapper;
        }

        public IViewComponentResult Invoke()
        {
            var parts = this.partsService.GetAllParts();
            var models = parts
                .Select(x => this.mapper.Map<PartViewModel>(x))
                .ToList();

            return View("Menu", models);
        }
    }
}
