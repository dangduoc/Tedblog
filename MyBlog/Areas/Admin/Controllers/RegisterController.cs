using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyBlog.Models;
using System.Web.Security;

namespace MyBlog.Areas.Admin.Controllers
{
    public class RegisterController : Controller
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        // GET: Admin/Register
        public ActionResult Login()
        {
            if (Session["current_user"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            return View(new Models.Security.UserLogin());
        }
        public ActionResult Logout()
        {
            if (Session["current_user"] != null)
            {
                FormsAuthentication.SignOut();
                Session.Abandon();
            }
            return View(new Models.Security.UserLogin());
        }
        [HttpPost]
        public ActionResult Login(Models.Security.UserLogin userLogOn)
        {
            if (ModelState.IsValid)
            {
                string password = this.getPassword(userLogOn.UserName);
                if (string.IsNullOrEmpty(password))
                {
                    ModelState.AddModelError("notexsist", "You don't have permission");
                }
                else
                {
                    if (password == userLogOn.Password)
                    {
                        var user = data.users.Where(_user => _user.username == userLogOn.UserName && _user.password == userLogOn.Password).FirstOrDefault();
                        if (user.role_id <= 1)
                        {
                            FormsAuthentication.SetAuthCookie(userLogOn.UserName, userLogOn.RememberMe);
                            Session["current_user"] = data.users.Where(_user => _user.username == userLogOn.UserName && _user.password == userLogOn.Password).FirstOrDefault();
                            return RedirectToAction("index", "Home", new { area="Admin"});
                        }
                        else
                        {
                            ModelState.AddModelError("role","You don't have permission");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("password","Password is incorrect");
                    }
                }
            }
            else
            ModelState.AddModelError("invalid", "Invalid Input");
            return View();
        }
        public string getPassword(string username)
        {
            using (MyBlogDBModel data = new MyBlogDBModel())
            {
                user userLogOn = data.users.Where(user => user.username == username).FirstOrDefault();
                if (userLogOn == null)
                {
                    return string.Empty;
                }
                else
                {
                    return userLogOn.password.Trim();
                }
            }
        }
    }
}