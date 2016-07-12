using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using MyBlog.Common;
namespace MyBlog.Areas.Admin.Controllers
{
    [CustomAthorize(Roles = "superadmin,admin")]
    public class BestMomentController : Controller
    {
        private MyBlogDBModel db = new MyBlogDBModel();

        // GET: Admin/BestMoment
        public ActionResult Index()
        {
            return View(db.BestMoments.ToList());
        }

        // GET: Admin/BestMoment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BestMoment bestMoment = db.BestMoments.Find(id);
            if (bestMoment == null)
            {
                return HttpNotFound();
            }
            return View(bestMoment);
        }

        // GET: Admin/BestMoment/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/BestMoment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "momentID,userID,Date_create,Image,Summary,Title")] BestMoment bestMoment)
        {
            if (ModelState.IsValid)
            {
                db.BestMoments.Add(bestMoment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(bestMoment);
        }

        // GET: Admin/BestMoment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BestMoment bestMoment = db.BestMoments.Find(id);
            if (bestMoment == null)
            {
                return HttpNotFound();
            }
            return View(bestMoment);
        }

        // POST: Admin/BestMoment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "momentID,userID,Date_create,Image,Summary,Title")] BestMoment bestMoment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bestMoment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bestMoment);
        }

        // GET: Admin/BestMoment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BestMoment bestMoment = db.BestMoments.Find(id);
            if (bestMoment == null)
            {
                return HttpNotFound();
            }
            return View(bestMoment);
        }

        // POST: Admin/BestMoment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            BestMoment bestMoment = db.BestMoments.Find(id);
            db.BestMoments.Remove(bestMoment);
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
