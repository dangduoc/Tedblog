using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
namespace MyBlog.Areas.Admin.Controllers
{
    [MyBlog.Common.CustomAthorize(Roles = "superadmin,admin")]
    public class UserController : Controller
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        // GET: Admin/User
        public ActionResult Index()
        {
            return View(data.users);
        }
        public JsonResult UserDetail(int id)
        {
            var u = data.userInfoes.Where(user => user.userID == id).FirstOrDefault();
            var role = data.userRoles.Where(user => user.roleID == data.users.Where(p => p.userID == id).FirstOrDefault().role_id).FirstOrDefault().role;
            return Json(new
            {
                ten = u.name,
                gioitinh = u.gender,
                diachi = u.addressLine,
                ngaytao = u.dateCreate.Value.ToShortDateString(),
                vaitro = role
            }
            , JsonRequestBehavior.AllowGet);
        }
    }
}