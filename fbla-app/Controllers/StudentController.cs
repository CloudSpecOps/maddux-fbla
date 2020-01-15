using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using fbla_app.data;

namespace fbla_app.Controllers
{
    public class StudentController : Controller
    {
        private fblaEntities db = new fblaEntities();

        public ActionResult Index()
        {
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            var view = db.vw_CommunityMembers
                .Where(c => c.PrimaryUserId == userId)
                .ToList<vw_CommunityMembers>();
            return View(view);
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student students = db.Students.Find(id);
            if (students == null)
            {
                return HttpNotFound();
            }
            return View(students);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentName,StudentID,Grade,CommunityId")] Student student)
        {
            if (ModelState.IsValid)
            {
                if (!string.IsNullOrEmpty(student.StudentName) && !string.IsNullOrEmpty(student.StudentName))
                {
                    db.Students.Add(student);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
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

            return View(student);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            var userId = Microsoft.AspNet.Identity.IdentityExtensions.GetUserId(User.Identity);
            ViewBag.Communities = db.Communities
                .Where(c => c.PrimaryUserId == userId)
                .Select(c => new SelectListItem
                {
                    Text = c.CommunityName,
                    Value = c.Id.ToString()
                })
                .ToList<SelectListItem>();
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,StudentName,StudentID,Grade,CommunityId")] Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
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
            return View(student);
        }

        // GET: Student/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        // POST: Student/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
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
