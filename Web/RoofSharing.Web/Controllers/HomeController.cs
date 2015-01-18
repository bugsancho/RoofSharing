namespace RoofSharing.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using RoofSharing.Data;
    using RoofSharing.Web.Controllers.Base;
    using Roofsharing.Services.Notifiers;

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
    }
}