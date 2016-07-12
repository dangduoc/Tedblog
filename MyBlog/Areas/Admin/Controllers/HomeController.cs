using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Common;
namespace MyBlog.Areas.Admin.Controllers
{
    [CustomAthorize(Roles = "superadmin,admin")]
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}