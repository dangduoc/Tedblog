using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
namespace MyBlog.Areas.Admin.Controllers
{
    [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
    public class TripController : Controller
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        // GET: Admin/Trip
        public ActionResult Index()
        {
            user crnt_user = (user)Session["current_user"];
            if (crnt_user.role_id <= 1)
            {
                return View(data.Trips);
            }
            else
            {
                return View(data.Trips.Where(t => t.userID == crnt_user.userID));
            }
        }
        [HttpPost]
        public void DeleteTrip(int id)
        {
            var tripid = Convert.ToInt32(id);
            var trip = data.Trips.Where(t => t.postID == tripid).FirstOrDefault();
            data.Trips.Remove(trip);
            data.SaveChanges();
        }
        [HttpPost]
        public ActionResult PendingTrips()
        {
            return View(data.previewTrips);
        }
        [HttpPost]
        public void DeletePendingTrip(string id)
        {
            var postid = Convert.ToInt32(id);
            var trip = data.previewTrips.Where(t => t.previewID == postid).FirstOrDefault();
            data.previewTrips.Remove(trip);
            data.SaveChanges();
        }
        [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        public ActionResult WriteTrip()
        {
            return View();
        }
        [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        [HttpPost, ValidateInput(false)]
        public ActionResult WriteTrip(FormCollection form)
        {
            if (!(String.IsNullOrEmpty(form["title"])) && !(String.IsNullOrEmpty(form["content"])) && (!String.IsNullOrEmpty(form["summary"])) && (!String.IsNullOrEmpty(form["thumbnail"])))
            {
                previewTrip newtrip = new previewTrip();
                var total = data.previewTrips.Count();
                if (total == 0)
                {
                    newtrip.previewID = 0;
                }
                else
                {
                    newtrip.previewID = data.previewTrips.OrderByDescending(p => p.previewID).FirstOrDefault().previewID + 1;
                }
                newtrip.postID = data.Trips.OrderByDescending(p => p.postID).FirstOrDefault().postID + 1;
                newtrip.date = DateTime.Now;
                newtrip.userID = ((user)Session["current_user"]).userID;
                newtrip.postTitle = form["title"];
                newtrip.postContent = form["content"];
                newtrip.postSummary = form["summary"];
                newtrip.imgThumbnail = form["thumbnail"];
                if (!String.IsNullOrEmpty(form["folder"]))
                {
                    newtrip.imageFolder = form["folder"];
                }
                data.previewTrips.Add(newtrip);
                data.SaveChanges();
                return Redirect("http://dangduoc.azurewebsites.net/FriendsandMe/PreviewTrip?id=" + newtrip.previewID);
            }
            else
                return View("Error");
        }
        [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin,friends")]
        public ActionResult EditTrip(int id)
        {
            var trip = data.Trips.Where(t => t.postID == id).FirstOrDefault();
            return View(trip);
        }
        public ActionResult PreviewTrip(int id)
        {
            var trip = data.previewTrips.Where(t => t.previewID == id).FirstOrDefault();
            return View(trip);
        }
        public ActionResult CheckEdit(int id)
        {
            user crnt_user = (user)Session["current_user"];
            var trip = data.Trips.Where(t => t.postID == id).FirstOrDefault();
            if (crnt_user.userID != trip.userID)
                return Json(false, JsonRequestBehavior.AllowGet);
            else
                return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}