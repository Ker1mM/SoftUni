using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Rider.Web.Areas.Identity.Pages.Account
{
    public class AccessDeniedModel : PageModel
    {
        public ActionResult OnGet()
        {
            return RedirectToAction("Error404", "Home");
        }
    }
}

