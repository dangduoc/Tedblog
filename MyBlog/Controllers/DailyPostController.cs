using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using PagedList;
namespace MyBlog.Controllers
{
    public class DailyPostController : Controller
    {
        //
        // GET: /DailyPost/
        private MyBlogDBModel data = new MyBlogDBModel();
        public ActionResult Index()
        {
            return View(data.DailyPosts.OrderByDescending(p => p.dateWrite));
        }
        [HttpPost]
        public ActionResult Index(string sortType)
        {
            string type = Request.Form["order-type"].ToString();
            var posts = data.DailyPosts.AsQueryable();
            ViewBag.selected = "Des";
            if (type == "Inscrease")
            {
                posts = data.DailyPosts.OrderByDescending(p => p.dateWrite);
                ViewBag.selected = "Ins";
            }
            else posts = data.DailyPosts;
            return View(posts);
        }
        public ActionResult Post(int id)
        {
            return View(data.DailyPosts.Where(p => p.postID == id).FirstOrDefault());
        }
    }
}
