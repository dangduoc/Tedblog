using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
namespace MyBlog.Areas.Admin.Controllers
{
    [MyBlog.Common.CustomAthorize(Roles = "superadmin")]
    public class MyPostController : Controller
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        // GET: Admin/MyPost
        public ActionResult Index()
        {
            return View(data.DailyPosts);
        }
        public ActionResult WritePost()
        {
            ViewBag.OK = TempData["writepost"];
            return View();
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult WritePost(FormCollection form)
        {
            if ((string.IsNullOrEmpty(form["title"]) || (string.IsNullOrEmpty(form["content"])) || (string.IsNullOrEmpty(form["date"]))))
            {
                TempData["writepost"] = "Input is invalid";
            }
            else
            {
                DailyPost newpost = new DailyPost();
                newpost.postID = data.DailyPosts.Count() + 1;
                newpost.postTitle = form["title"];
                newpost.postContent = form["content"].ToString();
                newpost.dateWrite = DateTime.Parse(form["date"]);
                data.DailyPosts.Add(newpost);
                data.SaveChanges();
                TempData["writepost"] = "Successfully Posted";
            }
            return RedirectToAction("WritePost");
        }
        public ActionResult EditPost(int id)
        {
            ViewBag.OK = TempData["editpost"];
            return View(data.DailyPosts.Where(p => p.postID == id).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult EditPost(FormCollection form)
        {
            DailyPost newpost = new DailyPost();
            newpost.postID = Convert.ToInt32(form["postid"]);
            newpost.postTitle = form["title"];
            newpost.postContent = form["content"].ToString();
            newpost.dateWrite = DateTime.Parse(form["date"]);
            DailyPost p = data.DailyPosts.Where(post => post.postID == newpost.postID).FirstOrDefault();
            p.dateWrite = newpost.dateWrite;
            p.postContent = newpost.postContent;
            p.postTitle = newpost.postTitle;
            data.SaveChanges();
            TempData["editpost"] = "Successfully Edited";
            return RedirectToAction("EditPost", new { id = p.postID });
        }
        [HttpPost]
        public string DeletePost(string id)
        {
            int idpost = Convert.ToInt32(id);
            DailyPost p = data.DailyPosts.Where(post => post.postID == idpost).FirstOrDefault();
            data.DailyPosts.Remove(p);
            data.SaveChanges();
            return "succeed";
        }
    }
}