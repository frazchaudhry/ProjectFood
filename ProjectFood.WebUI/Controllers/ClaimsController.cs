using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ProjectFood.WebUI.Controllers
{
    [Authorize]
    public class ClaimsController : Controller
    {
        // GET: Claims
        public ActionResult Index()
        {
            ClaimsIdentity ident = HttpContext.User.Identity as ClaimsIdentity;
            if (ident == null)
            {
                return View("Error", new string[] { "No Claims Available" });
            }
            else
            {
                return View(ident.Claims);
            }
            return View();
        }
    }
}