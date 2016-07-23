using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorController : Controller
    {
        // GET: Error
        public ActionResult Index(int id)
        {
            ViewBag.ErrorCode = id;
            Response.StatusCode = id;
            return View();
        }
    }
}