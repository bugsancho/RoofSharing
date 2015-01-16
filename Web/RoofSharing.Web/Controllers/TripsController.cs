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
using RoofSharing.Common;
using Roofsharing.Services.Notifiers;

namespace RoofSharing.Web.Controllers
{
    public class TripsController : BaseController
    {
        public TripsController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }

        // GET: Trips
        public ActionResult Index()
        {
            return View();
        }
        

        [Authorize]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new PublicTripViewModel()
            {
                HostId = this.CurrentUser.Id
            };
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(ICollection<string> participants, PublicTripViewModel model)
        {
            //TODO Find a better solution for Kendo MultiSelect binding.
            if (ModelState.ContainsKey("Participants"))
            {
                ModelState["Participants"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                var trip = Mapper.Map<PublicTrip>(model);
                if (participants != null && participants.Count != 0)
                {
                    trip.Participants = this.Data.Users.All().Where(user => participants.Contains(user.Id)).ToList();
                }

                this.Data.PublicTrips.Add(trip);
                this.Data.SaveChanges();
                TempData[GlobalConstants.SuccessMessage] = "Successfully created a new trip!";
                return RedirectToAction("View", new { id = trip.Id });
            }
            
            return View(model);
        }
        
        [HttpGet]
        public ActionResult View(int id)
        {
            var trip = this.Data.PublicTrips.All().Where(tr => tr.Id == id).Project().To<PublicTripViewModel>().FirstOrDefault();
            if (trip == null)
            {
                return HttpNotFound("Invalid Trip Id!");
            }
            return View(trip);
        }

        [Authorize]
        [HttpGet]
        public ActionResult Edit(int id)
        {
            var trip = this.Data.PublicTrips.All().Where(tr => tr.Id == id).Project().To<PublicTripViewModel>().FirstOrDefault();
            if (trip == null)
            {
                return HttpNotFound("Invalid trip Id");
            }

            if (trip.HostId != this.CurrentUser.Id)
            {
                return new HttpUnauthorizedResult("You can edit only trips that were initiated by you");
            }
            return View(trip);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Edit(ICollection<string> participants, PublicTripViewModel model)
        {
            if (model.HostId != this.CurrentUser.Id)
            {
                return new HttpUnauthorizedResult("You can edit only trips that were initiated by you");
            }

            //TODO Find a better solution for Kendo MultiSelect binding.
            if (ModelState.ContainsKey("Participants"))
            {
                ModelState["Participants"].Errors.Clear();
            }
            if (ModelState.IsValid)
            {
                var trip = this.Data.PublicTrips.Find(model.Id);
                if (trip == null)
                {
                    return HttpNotFound("Invalid trip Id");
                }
                Mapper.Map(model, trip);
                
                if (participants != null && participants.Count != 0)
                {
                    trip.Participants = this.Data.Users.All().Where(user => participants.Contains(user.Id)).ToList();
                }

                this.Data.SaveChanges();
                TempData[GlobalConstants.SuccessMessage] = "Successfully updated the trip info!";
                return RedirectToAction("View", new { id = trip.Id });
            }
            
            return View(model);
        }
    }
}