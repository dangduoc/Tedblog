using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using System.Web.Security;
namespace MyBlog.Controllers
{
    [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
    public class AdminController : Controller
    {     
        // GET: /Admin/
        private MyBlogDBModel data = new MyBlogDBModel();
        public ActionResult Index()
        {
            return View();
        }   
    }
}
