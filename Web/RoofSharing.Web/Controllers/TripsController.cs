using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.ViewModels.Trip;
using AutoMapper.QueryableExtensions;
using AutoMapper;
using RoofSharing.Data.Models;
using RoofSharing.Web.ViewModels.Account;

namespace RoofSharing.Web.Controllers
{
    public class TripsController : BaseController
    {
        public TripsController(IRoofSharingData data) : base(data)
        {
        }

        // GET: Trips
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult Plan()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Plan(PlanTripViewModel model)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Matches");
            }
            return View(model);
        }
        
        //TODO refactor to single string. Rename to searchCriteria
        public ActionResult Matches(PlanTripViewModel tripInfo)
        {
            if (tripInfo == null || !ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Please tell us first about your trip!");
                return RedirectToAction("Plan");
            }
            var results = this.Data.Users.All().Where(u => u.LocationInfo.City == tripInfo.SearchedCity && u.Id != this.CurrentUser.Id).Project().To<UserCardViewModel>().ToList();

            return View(results);
        }

        [Authorize]
        [HttpGet]
        public ActionResult CreatePublicTrip()
        {
            var model = new PublicTripViewModel()
            {
                HostId = this.CurrentUser.Id,
                StartPoint = this.CurrentUser.LocationInfo.City
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult CreatePublicTrip(ICollection<string> participants, PublicTripViewModel model)
        {
            //TODO Find a better solution for Kendo MultiSelect binding.
            ModelState["Participants"].Errors.Clear();
            if (ModelState.IsValid)
            {
                var trip = Mapper.Map<PublicTrip>(model);
                trip.Participants = this.Data.Users.All().Where(user => participants.Contains(user.Id)).ToList();
                this.Data.PublicTrips.Add(trip);
                this.Data.SaveChanges();
                return RedirectToAction("View", new { id = trip.Id });
            }
            
            return View(model);
        }
        
        [Authorize]
        [HttpGet]
        public ActionResult View(int id)
        {
            var trip = this.Data.PublicTrips.All().Where(tr => tr.Id == id).Project().To<PublicTripViewModel>().FirstOrDefault();
            if (trip == null)
            {
                throw new HttpException(400, "Invalid Trip Id!");
            }
            return View(trip);
        }
    }
}