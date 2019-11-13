using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Eventures.Web.ViewModels;
using Microsoft.AspNetCore.Diagnostics;
using System.Security;

namespace Eventures.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(ErrorViewModel model)
        {
            var exceptionFeature = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionFeature != null)
            {
                model.Message = SecurityElement.Escape(exceptionFeature.Error.Message);
            }

            return View(model);
        }

        [Route("/Home/Error/{code}")]
        public IActionResult ErrorCode(ErrorViewModel model)
        {
            return View("Error", model);
        }
    }
}
