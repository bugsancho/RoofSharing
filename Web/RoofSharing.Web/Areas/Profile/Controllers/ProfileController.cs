using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.Controllers;

namespace RoofSharing.Web.Areas.Profile.Controllers
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

    }
}