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
    public class GalleryHomeController : Controller
    {
        private MyBlogDBModel db = new MyBlogDBModel();

        // GET: Admin/GalleryHome
        public ActionResult Index()
        {
            return View(db.GalleryHomes.ToList());
        }

        // GET: Admin/GalleryHome/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryHome galleryHome = db.GalleryHomes.Find(id);
            if (galleryHome == null)
            {
                return HttpNotFound();
            }
            return View(galleryHome);
        }

        // GET: Admin/GalleryHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/GalleryHome/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,url,Title,dateCreate")] GalleryHome galleryHome)
        {
            if (ModelState.IsValid)
            {
                db.GalleryHomes.Add(galleryHome);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(galleryHome);
        }

        // GET: Admin/GalleryHome/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryHome galleryHome = db.GalleryHomes.Find(id);
            if (galleryHome == null)
            {
                return HttpNotFound();
            }
            return View(galleryHome);
        }

        // POST: Admin/GalleryHome/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,url,Title,dateCreate")] GalleryHome galleryHome)
        {
            if (ModelState.IsValid)
            {
                db.Entry(galleryHome).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(galleryHome);
        }

        // GET: Admin/GalleryHome/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            GalleryHome galleryHome = db.GalleryHomes.Find(id);
            if (galleryHome == null)
            {
                return HttpNotFound();
            }
            return View(galleryHome);
        }

        // POST: Admin/GalleryHome/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            GalleryHome galleryHome = db.GalleryHomes.Find(id);
            db.GalleryHomes.Remove(galleryHome);
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
