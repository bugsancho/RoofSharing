﻿namespace RoofSharing.Web.Controllers
{
    using System.Web.Mvc;
    
    public class ErrorController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }

        public ActionResult ServerError()
        {
            return View();
        }
    }
}