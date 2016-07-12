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
    public class SlideImageController : Controller
    {
        private MyBlogDBModel db = new MyBlogDBModel();

        // GET: Admin/SlideImage
        public ActionResult Index()
        {
            return View(db.SlideImages.ToList());
        }

        // GET: Admin/SlideImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // GET: Admin/SlideImage/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Admin/SlideImage/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,url,title,date")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.SlideImages.Add(slideImage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(slideImage);
        }

        // GET: Admin/SlideImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // POST: Admin/SlideImage/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,url,title,date")] SlideImage slideImage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(slideImage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(slideImage);
        }

        // GET: Admin/SlideImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SlideImage slideImage = db.SlideImages.Find(id);
            if (slideImage == null)
            {
                return HttpNotFound();
            }
            return View(slideImage);
        }

        // POST: Admin/SlideImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SlideImage slideImage = db.SlideImages.Find(id);
            db.SlideImages.Remove(slideImage);
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
