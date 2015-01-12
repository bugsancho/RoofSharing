using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.Controllers;
using AutoMapper.QueryableExtensions;
using RoofSharing.Data.Models.Profile;
using AutoMapper;
using RoofSharing.Web.ViewModels.Profile;

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class ProfileController : BaseController
    {
        public ProfileController(IRoofSharingData data) : base(data)
        {
        }

        [HttpGet]
        public ActionResult Index(string userId = null)
        {
            bool fullProfileAllowed = false;
            string currentUserId = this.CurrentUser.Id;
            string id = currentUserId;
            if (userId != null)
            {
                fullProfileAllowed = this.Data.Friendships.All().Any(f => ((f.FromUserId == currentUserId && f.ToUserId == userId) || (f.ToUserId == currentUserId && f.FromUserId == userId)) && f.Status == RoofSharing.Data.Models.FriendshipStatusType.Friends);
                id = userId;
            }
            else
            {
                fullProfileAllowed = true;
            }
            var model = new ProfileInfoModel()
            {
                FullProfileAllowed = fullProfileAllowed || userId == currentUserId,
                Id = id
            };
            
            //var model = this.Data.Users.All().Where(u => u.Id == queryUserId).Project().To<ProfileSummaryViewModel>().FirstOrDefault();
            return View("Index", null, model);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        
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