using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoofSharing.Web.Areas.Administration.Controllers
{
    public class AdminHomeController : Controller
    {
        // GET: Administration/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}