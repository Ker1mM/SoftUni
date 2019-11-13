using FDMC.Services;
using FDMC.ViewModels.Cats;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FDMC.Controllers
{
    public class CatsController : Controller
    {
        private readonly ICatsService catsService;

        public CatsController(ICatsService catsService)
        {
            this.catsService = catsService;
        }

        [HttpGet("/cats/{id}")]
        public IActionResult Details(int id)
        {
            var cat = this.catsService.GetCatById(id);

            var model = new CatDetailsViewModel
            {
                Name = cat.Name,
                Age = cat.Age,
                ImageURL = cat.ImageURL,
                Breed = cat.Breed
            };

            return this.View(model);
        }


        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Add(AddCatInputModel model)
        {

            if (!ModelState.IsValid)
            {
                return this.Redirect("/cats/add");
            }
            
            this.catsService.AddCat(model.Name, model.Age, model.Breed, model.ImageURL);

            return this.Redirect("/");
        }
    }
}
