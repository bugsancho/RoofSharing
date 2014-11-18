using RoofSharing.Data;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Areas.Profile.ViewModels;
using RoofSharing.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    public class HousingController : BaseController
    {
        public HousingController(IRoofSharingData data) : base(data)
        {
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult Update()
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
            
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(HousingViewModel model)
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
        [ChildActionOnly]
        public ActionResult HousingProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.HousingInfo).Project().To<HousingViewModel>().FirstOrDefault();

            return PartialView("~/Areas/Profile/Views/Shared/_HousingProfile.cshtml", user);
        }
    }
}