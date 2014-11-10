using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoofSharing.Web.Filters
{
    public class WelcomePageFilter : IActionFilter
    {
        private const string BeenHereBeforeCookieName = "Visited-RoofSharing-Before";

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var requestCookie = filterContext.HttpContext.Request.Cookies.Get(BeenHereBeforeCookieName);
            if (requestCookie == null)
            {
                var responseCookie = new HttpCookie(BeenHereBeforeCookieName, "true");
                filterContext.HttpContext.Response.Cookies.Set(responseCookie);
                filterContext.HttpContext.Response.RedirectToRoute(new { controller = "Home", action = "Welcome" });
            }
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            
        }
    }
}