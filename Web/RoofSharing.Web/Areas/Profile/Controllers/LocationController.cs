using RoofSharing.Data;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Areas.Profile.ViewModels;
using RoofSharing.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper.QueryableExtensions;
using AutoMapper.Mappers;
using System.Web.Mvc;
using AutoMapper;
using RoofSharing.Web.Infrastructure.ValidationAttributes;

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class LocationController : BaseController
    {
        public LocationController(IRoofSharingData data) : base(data)
        {
        }
        
        [HttpGet]
        public ActionResult Update()
        {
            var location = Mapper.Map<LocationViewModel>(this.CurrentUser.LocationInfo);
            return View(location);
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Update(LocationViewModel locationInfo)
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

        [ChildActionOnly]
        [OutputCache(Duration = 60)]
        public ActionResult LocationProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.LocationInfo).Project().To<LocationViewModel>().FirstOrDefault();

            return PartialView("~/Areas/Profile/Views/Shared/_LocationProfile.cshtml", user);
        }
    }
}