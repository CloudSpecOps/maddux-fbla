using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fbla_app.data;
using fbla_app.Models;

namespace fbla_app.Controllers
{
    public class StudentHourController : Controller
    {
        private fblaEntities db = new fblaEntities();

        // GET: StudentHour
        public ActionResult Index()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            var model = db.vw_StudentHours
                .Where(sh => sh.PrimaryUserId == userId)
                .ToList<vw_StudentHours>();
            return View(model);
        }

        // GET: StudentHour/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHour studentHour = db.StudentHours.Find(id);
            if (studentHour == null)
            {
                return HttpNotFound();
            }
            return View(studentHour);
        }

        public ActionResult HoursReport()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult HoursReport([Bind(Include = "StartDate,EndDate")] HoursReportViewModel studentHours)
        {
            if (ModelState.IsValid)
            {
                var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
                studentHours.StudentHours = db.vw_StudentHours
                    .Where(sh => sh.PrimaryUserId == userId 
                        && sh.ServiceDate >= studentHours.StartDate
                        && sh.ServiceDate <= studentHours.EndDate)
                    .Select(sh => new StudentHoursReportViewModel
                    {
                        CommunityHours = sh.CommunityHours,
                        CommunityId = sh.CommunityId,
                        CommunityName = sh.CommunityName,
                        Grade = sh.Grade,
                        PrimaryUserId = sh.PrimaryUserId,
                        ServiceDate = sh.ServiceDate,
                        StudentCode = sh.StudentCode,
                        StudentId = sh.StudentId,
                        StudentName = sh.StudentName
                    })
                    .ToList<StudentHoursReportViewModel>();

                foreach (var item in studentHours.StudentHours)
                {
                    item.ServiceAward = db.ServiceAwards
                        .Where(s => s.ServiceThreshhold >= item.CommunityHours
                        && s.ServiceThreshhold <= item.CommunityHours)
                        .Select(s => s.ServiceAwardName).SingleOrDefault<string>();
                }
            }

            return View(studentHours);
        }

        public ActionResult Create()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            ViewBag.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .Select(c => new SelectListItem
                {
                    Text = c.CommunityName,
                    Value = c.Id.ToString()
                })
                .ToList<SelectListItem>();
            return View();
        }

        public ActionResult CSAReport()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            var model = db.vw_StudentHoursTotals
                .Where(s => s.PrimaryUserId == userId)
                .ToList<vw_StudentHoursTotals>();

            foreach (var item in model)
            {
                item.ServiceAward = db.ServiceAwards
                    .Where(s => item.TotalHours >= s.ServiceThreshhold)
                    .OrderByDescending(s => s.ServiceThreshhold)
                    .Select(s => s.ServiceAwardName)
                    .FirstOrDefault();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommunityId,StudentId,ServiceDate,CommunityHours,ServiceThreshhold")] StudentHour studentHour)
        {
            if (ModelState.IsValid)
            {
                db.StudentHours.Add(studentHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            ViewBag.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .Select(c => new SelectListItem
                {
                    Text = c.CommunityName,
                    Value = c.Id.ToString()
                })
                .ToList<SelectListItem>();

            return View(studentHour);
        }

        // GET: StudentHour/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHour studentHour = db.StudentHours.Find(id);
            if (studentHour == null)
            {
                return HttpNotFound();
            }

            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            ViewBag.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .Select(c => new SelectListItem
                {
                    Text = c.CommunityName,
                    Value = c.Id.ToString()
                })
                .ToList<SelectListItem>();

            return View(studentHour);
        }

        // POST: StudentHour/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentId,ServiceDate,CommunityHours,ServiceThreshhold")] StudentHour studentHour)
        {
            if (ModelState.IsValid)
            {
                db.Entry(studentHour).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            ViewBag.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .Select(c => new SelectListItem
                {
                    Text = c.CommunityName,
                    Value = c.Id.ToString()
                })
                .ToList<SelectListItem>();

            return View(studentHour);
        }

        public ActionResult FillStudents(int CommunityId)
        {
            var students = db.Students
                .Where(c => c.CommunityId == CommunityId)
                .Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.StudentName
                }).ToList<SelectListItem>();
            return Json(students, JsonRequestBehavior.AllowGet);
        }

        // GET: StudentHour/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            StudentHour studentHour = db.StudentHours.Find(id);
            if (studentHour == null)
            {
                return HttpNotFound();
            }
            return View(studentHour);
        }

        // POST: StudentHour/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            StudentHour studentHour = db.StudentHours.Find(id);
            db.StudentHours.Remove(studentHour);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
