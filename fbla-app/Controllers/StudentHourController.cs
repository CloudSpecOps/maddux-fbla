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
    public class StudentHourController : Controller
    {
        private fblaEntities db = new fblaEntities();

        // GET: StudentHour
        public ActionResult Index()
        {
            return View(db.StudentHours.ToList());
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

        // GET: StudentHour/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: StudentHour/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,StudentId,ServiceDate,CommunityHours,ServiceThreshhold")] StudentHour studentHour)
        {
            if (ModelState.IsValid)
            {
                db.StudentHours.Add(studentHour);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            return View(studentHour);
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
