using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Controllers
{
    public class ErrorPageController : Controller
    {
        // GET: ErrorPage
        public ActionResult Oops(int id)
        {
            Response.StatusCode = id;
            ViewBag.ErrorCode = id;
            return View();
        }
    }
}