using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
using System.Web.Mvc;
namespace MyBlog.Common
{
    public class Rules
    {
        private MyBlogDBModel data = new MyBlogDBModel();
        public static bool checkUsertoUpload()
        {
            var user = (user)HttpContext.Current.Session["current_user"];
            if (user == null) return false;
            else
            return (user.role_id <= 2);
        }
    }
}