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
    }
}