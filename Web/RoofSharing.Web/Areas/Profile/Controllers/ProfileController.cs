using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.Controllers;
using RoofSharing.Web.Areas.Profile.ViewModels;
using AutoMapper.QueryableExtensions;
using RoofSharing.Data.Models.Profile;
using AutoMapper;

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
            var model = this.Data.Users.All().Where(u => u.Id == this.CurrentUser.Id).Project().To<ProfileSummaryViewModel>().FirstOrDefault();
            return View("Index", null, this.CurrentUser.Id);
        }
        
        

        [ChildActionOnly]
        public ActionResult ProfileSummary(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Project().To<ProfileSummaryViewModel>().FirstOrDefault();
            if (user == null)
            {
                return HttpNotFound("User with that Id could not be found!");
            }
            return PartialView("~/Areas/Profile/Views/Shared/_ProfileSummary.cshtml", user);
        }
    }
}