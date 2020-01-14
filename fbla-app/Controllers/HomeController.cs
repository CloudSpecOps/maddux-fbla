using fbla_app.data;
using fbla_app.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace fbla_app.Controllers
{
    public class HomeController : Controller
    {
        private fblaEntities db = new fblaEntities();

        public ActionResult Index()
        {
            HomeViewModel view = new HomeViewModel();
            view.Communities = db.Communities.ToList();
            return View(view);
        }
    }
}