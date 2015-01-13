using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.ViewModels.Profile;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RoofSharing.Data.Models.Profile;

namespace RoofSharing.Web.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IRoofSharingData data) : base(data)
        {
        }

        // GET: Profile
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateHouse()
        {
            var model = this.Data.Users.All().Where(u => u.Id == this.CurrentUser.Id).Select(user => user.HousingInfo);
            HousingViewModel viewModel = null;
            if (model.FirstOrDefault() == null)
            {
                viewModel = new HousingViewModel();
            }
            else
            {
                viewModel = model.Project().To<HousingViewModel>().FirstOrDefault();
            }
            
            return PartialView(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHouse(HousingViewModel model)
        {
            try
            {
                this.CurrentUser.HousingInfo = Mapper.Map<UserHousingInfo>(model);
                this.Data.SaveChanges();
                return RedirectToAction("Index", "Profile");
            }
            catch
            {
                return View(model);
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateLocation()
        {
            var location = Mapper.Map<LocationViewModel>(this.CurrentUser.LocationInfo);
            return PartialView(location);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateLocation(LocationViewModel locationInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var location = Mapper.Map<LocationInfo>(locationInfo);
                    this.CurrentUser.LocationInfo = location;
                    this.Data.SaveChanges();

                    return RedirectToAction("Index", "Profile");
                }
                catch
                {
                    return View(locationInfo);
                }
            }

            return View(locationInfo);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdatePersonality()
        {
            var model = this.Data.Users.All().Where(u => u.Id == this.CurrentUser.Id).Select(user => user.PersonalityInfo);
            PersonalityViewModel viewModel = null;
            if (model.FirstOrDefault() == null)
            {
                viewModel = new PersonalityViewModel() { FirstName = this.CurrentUser.FirstName, LastName = this.CurrentUser.LastName };
            }
            else
            {
                viewModel = model.Project().To<PersonalityViewModel>().FirstOrDefault();
            }
            
            return PartialView(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UpdatePersonality(PersonalityViewModel personalityInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    this.CurrentUser.PersonalityInfo = Mapper.Map<PersonalityInfo>(personalityInfo);
                    this.CurrentUser.FirstName = personalityInfo.FirstName;
                    this.CurrentUser.LastName = personalityInfo.LastName;
                    this.Data.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }

                return RedirectToAction("Index", "Profile");
            }
            
            return View(personalityInfo);
        }


         public ActionResult HousingProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.HousingInfo).Project().To<HousingViewModel>().FirstOrDefault();

            return PartialView("~/Views/Profile/_HousingProfile.cshtml", user);
        }

         public ActionResult LocationProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.LocationInfo).Project().To<LocationViewModel>().FirstOrDefault();

            return PartialView("~/Views/Profile/_LocationProfile.cshtml", user);
        }

          public ActionResult PersonalityProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.PersonalityInfo).Project().To<PersonalityViewModel>().FirstOrDefault();

            return PartialView("~/Views/Profile/_PersonalityProfile.cshtml", user);
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
            return PartialView("~/Views/Profile/_ProfileSummary.cshtml", user);
        }
    }
}