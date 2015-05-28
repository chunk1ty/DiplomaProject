namespace Ankk.Web.Controllers
{
    using Ankk.Data;
    using Ankk.Models;
    using Ankk.Web.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    
    [Authorize(Roles = "Administrator")]
    public class AdministrationController : Controller
    {
        private IAnkkData data = new AnkkData();
        
        public ActionResult Index()
        {
            ViewBag.Context = "Administrator area";
            return View();
        }

        //ContestCOntroller
        [HttpGet]
        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ContestViewModel model)
        {
            if (model != null && this.ModelState.IsValid)
            {
                Contest contest = new Contest()
                {
                    Id = model.Id,
                    Name = model.Name,
                    IsVisible = model.IsVisible
                };

                this.data.Contests.Add(contest);
                this.data.SaveChanges();

                return this.RedirectToAction("Index");
            }

            return this.View(model);
        }
    }
}