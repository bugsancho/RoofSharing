using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;

namespace RoofSharing.Web.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IRoofSharingData data) : base(data)
        {
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View(CurrentUser);
        }

        [HttpGet]
        public ActionResult CompleteProfile()
        {

            return View();
        }
    }
}