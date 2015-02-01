namespace RoofSharing.Web.Controllers
{
    using System;
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RoofSharing.Common;
    using RoofSharing.Data;
    using RoofSharing.Web.Controllers.Base;
    using RoofSharing.Web.ViewModels.Profile;
    using Roofsharing.Services.Notifiers;
    using RoofSharing.Web.Infrastructure.Helpers;
    using RoofSharing.Web.Infrastructure.Extensions;

    public class ProfileController : BaseController
    {
        private const string ProfileUpdatedMessage = "Profile updated successfully!";

        public ProfileController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
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
            var model = this.Data.Users.All()
                            .Where(u => u.Id == this.CurrentUser.Id)
                            .Select(user => user.HousingInfo)
                            .Project()
                            .To<HousingViewModel>()
                            .FirstOrDefault();
            
            return PartialView(model);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UpdateHouse(HousingViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = this.Data.Users.All().Where(x => x.Id == this.CurrentUser.Id).FirstOrDefault();
                Mapper.Map(model, user.HousingInfo);

                this.Data.SaveChanges();
                this.TempData[GlobalConstants.SuccessMessage] = ProfileUpdatedMessage;

                return RedirectToAction("Index", "Profile");
            }

            return View(model);
        }

        [Authorize]
        [HttpGet]
        public ActionResult UpdateLocation()
        {
            var model = this.Data.Users.All()
                            .Where(u => u.Id == this.CurrentUser.Id)
                            .Select(user => user.LocationInfo)
                            .Project()
                            .To<LocationViewModel>()
                            .FirstOrDefault();
            
            return PartialView(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult UpdateLocation(LocationViewModel locationInfo)
        {
            if (ModelState.IsValid)
            {
                var user = this.Data.Users.All().Where(x => x.Id == this.CurrentUser.Id).FirstOrDefault();
                Mapper.Map(locationInfo, user.LocationInfo);

                this.Data.SaveChanges();
                this.TempData[GlobalConstants.SuccessMessage] = ProfileUpdatedMessage;
                    
                return RedirectToAction("Index", "Profile");
            }

            return View(locationInfo);
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdatePersonality()
        {
            var model = this.Data.Users.All()
                            .Where(u => u.Id == this.CurrentUser.Id)
                            .Select(user => user.PersonalityInfo)
                            .Project()
                            .To<PersonalityViewModel>()
                            .FirstOrDefault();
            
            return PartialView(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult UpdatePersonality(PersonalityViewModel personalityInfo)
        {
            if (ModelState.IsValid)
            {
                var user = this.Data.Users.All().Where(x => x.Id == this.CurrentUser.Id).FirstOrDefault();
                Mapper.Map(personalityInfo, user.PersonalityInfo);    
            
                this.Data.SaveChanges();
                this.TempData[GlobalConstants.SuccessMessage] = ProfileUpdatedMessage;

                return RedirectToAction("Index", "Profile");
            }
            
            return View(personalityInfo);
        }

        [HttpGet]
        [Authorize]
        public ActionResult ChangePicture()
        {
            string pictureUrl = this.CurrentUser.PictureUrl;

            return PartialView("_ChangePicturePartial", pictureUrl);
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePicture(HttpPostedFileBase uploadedPicture)
        {
            if (uploadedPicture != null)
            {
                try
                {
                    this.CurrentUser.PictureUrl = ImgurPictureUploader.HandlePostedPicture(uploadedPicture);
                    this.Data.SaveChanges();
                }
                catch (Exception e)
                {
                    //TempData[GlobalConstants.ErrorMessage] = "There was a problem uploading your image!";
                    return RedirectToAction("ChangePicture");
                }
            }
            else
            {
                return RedirectToAction("ChangePicture");
            }
            
            return Json(new { success = "True" });
        }

        [HttpGet]
        [Authorize]
        public ActionResult UpdateAccountInfo()
        {
            //TODO fix refresh error!
            ViewData["HasPassword"] = !string.IsNullOrEmpty(this.Data.Users.Find(this.CurrentUser.Id).PasswordHash);

            return PartialView("~/Views/Profile/_UpdateAccountInfoPartial.cshtml");
        }

        public ActionResult HousingProfile(string userId)
        {
            var user = this.Data.Users.All()                
                           .Where(u => u.Id == userId)
                           .Select(u => u.HousingInfo)
                           .Project()
                           .To<HousingViewModel>()
                           .FirstOrDefault();

            return PartialView("~/Views/Profile/_HousingProfile.cshtml", user);
        }
        
        public ActionResult LocationProfile(string userId)
        {
            var user = this.Data.Users.All()
                           .Where(u => u.Id == userId)
                           .Select(u => u.LocationInfo)
                           .Project()
                           .To<LocationViewModel>()
                           .FirstOrDefault();

            return PartialView("~/Views/Profile/_LocationProfile.cshtml", user);
        }

        public ActionResult PersonalityProfile(string userId)
        {
            var user = this.Data.Users.All()
                           .Where(u => u.Id == userId)
                           .Select(u => u.PersonalityInfo)
                           .Project()
                           .To<PersonalityViewModel>()
                           .FirstOrDefault();

            return PartialView("~/Views/Profile/_PersonalityProfile.cshtml", user);
        }
    }
}