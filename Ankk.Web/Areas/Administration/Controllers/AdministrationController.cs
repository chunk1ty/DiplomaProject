using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ankk.Web.Areas.Administration.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        // GET: Administration
        public ActionResult Index()
        {
            ViewBag.Context = "Administrator area";
            return View();
        }
    }
}