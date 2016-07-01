using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using PagedList;

namespace MyBlog.Controllers
{
    public class FriendsandMeController : Controller
    {
        //
        // GET: /FriendsandMe/
        private MyBlogDBModel data = new MyBlogDBModel();
        public ActionResult Trips()
        {
            ViewBag.lastests = data.Trips.OrderByDescending(p => p.date).Take(4);
            return View(data.Trips.OrderByDescending(p => p.date));
        }      
        public ActionResult Trip(int id)
        {
            var trip = data.Trips.Where(t => t.postID == id).FirstOrDefault();
            ViewBag.others = data.Trips.OrderByDescending(p=>p.date).Take(5);
            return View(trip);
        }
    
        public ActionResult Moment(int id)
        {
            return View(data.BestMoments.Where(p => p.momentID == id).FirstOrDefault());
        }
        public ActionResult Moments()
        {
            var moments=data.BestMoments.OrderByDescending(p => p.Date_create).ToList<BestMoment>();
            List<List<BestMoment>> list = new List<List<BestMoment>>();
            for (int i = 0; i < moments.Count; i=i+3)
            {
                int j = i;
                List<BestMoment> tmp = new List<BestMoment>();
                while (((j - i) <= 2)&&(j<moments.Count))
                {
                    tmp.Add(moments[j]);
                    j++;
                }
                list.Add(tmp);
            }
            ViewBag.list = list;
            return View();

        }
        //-------------------------------------------
        [MyBlog.Common.CustomAthorize(Roles="friends,admin,superadmin")]
        [HttpPost]
        public void Post(BestMoment newmoment)
        {
                var crnt_user = (Models.user)Session["current_user"];
                var newpost = new BestMoment();
                newpost.Date_create = DateTime.Now;
                newpost.momentID = data.BestMoments.OrderByDescending(p=>p.momentID).FirstOrDefault().momentID+1;
                newpost.Title = newmoment.Title;
                newpost.Image = newmoment.Image;
                newpost.Summary = newmoment.Summary;
                newpost.userID=crnt_user.userID;
                data.BestMoments.Add(newpost);
                data.SaveChanges();
        }
    }
}
