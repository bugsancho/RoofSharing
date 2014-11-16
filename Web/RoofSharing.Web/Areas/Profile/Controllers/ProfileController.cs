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
            return View(CurrentUser);
        }
        
        [HttpGet]
        [Authorize]
        public ActionResult Personality()
        {
            var model = this.Data.Users.All().Where(u => u.Id == this.CurrentUser.Id).Select(user => user.PersonalityInfo);
            PersonalityViewModel viewModel = null;
            if (model.FirstOrDefault() == null)
            {
                viewModel = new PersonalityViewModel();
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
        public ActionResult Personality(PersonalityViewModel personalityInfo)
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