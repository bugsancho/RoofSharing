using System;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RoofSharing.Data;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.ViewModels.Profile;
using Roofsharing.Services.Notifiers;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace RoofSharing.Web.Controllers
{
    public class ProfileController : BaseController
    {
        public ProfileController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
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

        [HttpGet]
        [Authorize]
        public ActionResult UpdateAccountInfo()
        {
            //TODO fix refresh error!
            ViewData["HasPassword"] = !string.IsNullOrEmpty(this.Data.Users.Find(this.CurrentUser.Id).PasswordHash);

            return PartialView("~/Views/Profile/_UpdateAccountInfoPartial.cshtml");
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
        [Authorize]
        public ActionResult Index(string userId = null)
        {
            var profileInfo = this.Data.Users.All()
                                  .Where(x => x.Id == (userId ?? this.CurrentUser.Id))
                                  .Project()
                                  .To<ProfileOverviewViewModel>()
                                  .FirstOrDefault();

            bool isOwnProfile = true;
            if (userId != null && userId != this.CurrentUser.Id)
            {
                isOwnProfile = false;
            }

            ViewBag.IsOwnProfile = isOwnProfile;

            return View(profileInfo);
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