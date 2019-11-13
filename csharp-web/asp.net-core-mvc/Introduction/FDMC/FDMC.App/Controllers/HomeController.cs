using FDMC.Services;
using FDMC.ViewModels.Cats;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICatsService catsService;

        public HomeController(ICatsService catsService)
        {
            this.catsService = catsService;
        }

        public IActionResult Index()
        {
            var cats = catsService.GetAllCats();
            var model = cats.Select(x => new AllCatsViewModel
            {
                Id = x.Id,
                Name = x.Name
            })
                .ToList();
            return this.View(model);
        }
    }
}
