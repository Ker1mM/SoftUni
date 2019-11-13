using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PANDA.App.ViewModels;
using PANDA.Services.Contracts;

namespace PANDA.App.Controllers
{
    public class PackageController : Controller
    {
        private readonly IUserServices userServices;

        public PackageController(IUserServices userServices)
        {
            this.userServices = userServices;
        }

        public IActionResult Create()
        {
            var users = userServices.GetAllUsers();

            var recModel = new RecipientViewModel
            {
                Users = users
            };

            var model = new CreatePackageInputModel
            {
                Recipients = recModel
            };

            return View(model);
        }

        //[HttpPost]
        //public IActionResult Create(CreatePackageInputModel model)
        //{

        //}
    }
}