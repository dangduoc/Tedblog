using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyBlog.Common
{
    public class CustomAthorizeAttribute:AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            var current_user = (Models.user)HttpContext.Current.Session["current_user"];
            if (current_user == null)
            {
                filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Register", Action = "index" }));
            }
            else
            {
                CustomPrincipal cp = new CustomPrincipal(current_user);
                if (!cp.IsInRole(Roles))
                {
                    filterContext.Result = new RedirectToRouteResult(new System.Web.Routing.RouteValueDictionary(new { Controller = "Register", Action = "index" }));
                }
               
            }
        }
    }
}