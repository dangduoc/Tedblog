using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyBlog.Models;
namespace MyBlog.Common
{
    public class Methods
    {
        private static MyBlogDBModel data = new MyBlogDBModel();
        public static string findusernameByID(int id)
        {
            return data.users.Where(u => u.userID == id).FirstOrDefault().username;
        }
    }
}