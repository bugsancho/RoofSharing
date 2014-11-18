using RoofSharing.Data;
using RoofSharing.Web.Areas.Profile.ViewModels;
using RoofSharing.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using RoofSharing.Data.Models.Profile;

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    public class PersonalityController : BaseController
    {
        public PersonalityController(IRoofSharingData data) : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public ActionResult Update()
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
            
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Update(PersonalityViewModel personalityInfo)
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

        [Authorize]
        [ChildActionOnly]
        public ActionResult PersonalityProfile(string userId)
        {
            var user = this.Data.Users.All().Where(u => u.Id == userId).Select(u => u.PersonalityInfo).Project().To<PersonalityViewModel>().FirstOrDefault();

            return PartialView("~/Areas/Profile/Views/Shared/_PersonalityProfile.cshtml", user);
        }
    }
}