using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoofSharing.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController()
        {

        }
        public ActionResult Index()
        {
            if (this.CurrentUser != null)
            {
                ViewBag.pic = this.CurrentUser.PictureUrl;
            }
            
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}