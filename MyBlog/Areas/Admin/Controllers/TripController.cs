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
    public class TripController : Controller
    {
        private MyBlogDBModel db = new MyBlogDBModel();

        // GET: Admin/Trip
        public ActionResult Index()
        {
            return View(db.Trips.ToList());
        }

        // GET: Admin/Trip/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // GET: Admin/Trip/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/Trip/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "postID,userID,postTitle,postSummary,date,postContent,friends,imgThumbnail,imageFolder,metaImage,metaDescription")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(trip);
        }

        // GET: Admin/Trip/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Admin/Trip/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "postID,userID,postTitle,postSummary,date,postContent,friends,imgThumbnail,imageFolder,metaImage,metaDescription")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(trip);
        }

        // GET: Admin/Trip/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Admin/Trip/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
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
