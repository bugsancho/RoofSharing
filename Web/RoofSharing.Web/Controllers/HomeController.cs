using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using Roofsharing.Services.Common.Notifiers;

namespace RoofSharing.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }


        public ActionResult Index()
        { 
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

        public ActionResult Welcome()
        { 
            return View();
        }
    }
}