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
    public class MyPostController : Controller
    {
        private MyBlogDBModel db = new MyBlogDBModel();

        // GET: Admin/MyPost
        public ActionResult Index()
        {
            return View(db.DailyPosts.OrderByDescending(p=>p.postID).ToList());
        }

        // GET: Admin/MyPost/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPost dailyPost = db.DailyPosts.Find(id);
            if (dailyPost == null)
            {
                return HttpNotFound();
            }
            return View(dailyPost);
        }

        // GET: Admin/MyPost/Create
        public ActionResult Create()
        {
            int id=db.DailyPosts.OrderByDescending(p => p.postID).FirstOrDefault().postID + 1;
            ViewBag.id = id;
            return View();
        }

        // POST: Admin/MyPost/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "postID,postTitle,postContent,dateWrite")] DailyPost dailyPost)
        {
            if (ModelState.IsValid)
            {
                db.DailyPosts.Add(dailyPost);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(dailyPost);
        }

        // GET: Admin/MyPost/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPost dailyPost = db.DailyPosts.Find(id);
            if (dailyPost == null)
            {
                return HttpNotFound();
            }
            return View(dailyPost);
        }

        // POST: Admin/MyPost/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "postID,postTitle,postContent,dateWrite")] DailyPost dailyPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dailyPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(dailyPost);
        }

        // GET: Admin/MyPost/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DailyPost dailyPost = db.DailyPosts.Find(id);
            if (dailyPost == null)
            {
                return HttpNotFound();
            }
            return View(dailyPost);
        }

        // POST: Admin/MyPost/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DailyPost dailyPost = db.DailyPosts.Find(id);
            db.DailyPosts.Remove(dailyPost);
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
