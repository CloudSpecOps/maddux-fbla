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
    public class CommunityController : Controller
    {
        private fblaEntities db = new fblaEntities();

        // GET: Community
        public ActionResult Index()
        {
            return View(db.Communities.ToList());
        }

        // GET: Community/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // GET: Community/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Community/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,CommunityName,PrimaryUserId")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Communities.Add(community);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(community);
        }

        // GET: Community/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: Community/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CommunityName,PrimaryUserId")] Community community)
        {
            if (ModelState.IsValid)
            {
                db.Entry(community).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(community);
        }

        // GET: Community/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Community community = db.Communities.Find(id);
            if (community == null)
            {
                return HttpNotFound();
            }
            return View(community);
        }

        // POST: Community/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Community community = db.Communities.Find(id);
            db.Communities.Remove(community);
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
