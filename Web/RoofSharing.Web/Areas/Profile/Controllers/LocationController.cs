using RoofSharing.Data;
using RoofSharing.Data.Models.Profile;
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
using RoofSharing.Web.ViewModels.Profile;

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class LocationController : BaseController
    {
        public LocationController(IRoofSharingData data) : base(data)
        {
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