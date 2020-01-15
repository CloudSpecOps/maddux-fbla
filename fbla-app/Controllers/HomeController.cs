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
            HomeViewModel model = new HomeViewModel();
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            model.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .ToList<Community>();
            model.RecentStudentHours = db.vw_StudentHours
                .Where(sh => sh.PrimaryUserId == userId)                
                .OrderByDescending(sh => sh.ServiceDate)
                .Take(10)
                .ToList<vw_StudentHours>();
            return View(model);
        }
    }
}