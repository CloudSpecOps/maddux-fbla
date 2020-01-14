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
    public class ServiceAwardController : Controller
    {
        private fblaEntities db = new fblaEntities();

        // GET: ServiceAward
        public ActionResult Index()
        {
            return View(db.ServiceAwards.ToList());
        }

        // GET: ServiceAward/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAward serviceAward = db.ServiceAwards.Find(id);
            if (serviceAward == null)
            {
                return HttpNotFound();
            }
            return View(serviceAward);
        }

        // GET: ServiceAward/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ServiceAward/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommunityId,ServiceAwardName,ServiceThreshhold")] ServiceAward serviceAward)
        {
            if (ModelState.IsValid)
            {
                db.ServiceAwards.Add(serviceAward);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(serviceAward);
        }

        // GET: ServiceAward/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAward serviceAward = db.ServiceAwards.Find(id);
            if (serviceAward == null)
            {
                return HttpNotFound();
            }
            return View(serviceAward);
        }

        // POST: ServiceAward/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommunityId,ServiceAwardName,ServiceThreshhold")] ServiceAward serviceAward)
        {
            if (ModelState.IsValid)
            {
                db.Entry(serviceAward).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(serviceAward);
        }

        // GET: ServiceAward/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ServiceAward serviceAward = db.ServiceAwards.Find(id);
            if (serviceAward == null)
            {
                return HttpNotFound();
            }
            return View(serviceAward);
        }

        // POST: ServiceAward/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ServiceAward serviceAward = db.ServiceAwards.Find(id);
            db.ServiceAwards.Remove(serviceAward);
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
