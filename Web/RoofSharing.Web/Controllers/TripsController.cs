namespace RoofSharing.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;
    using AutoMapper;
    using AutoMapper.QueryableExtensions;
    using RoofSharing.Common;
    using RoofSharing.Data;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Controllers.Base;
    using RoofSharing.Web.ViewModels.Trip;
    using Roofsharing.Services.Notifiers;
    
    public class TripsController : BaseController
    {
        private const int PageSize = 4;

        public TripsController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }

        // GET: Trips
        public ActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public ActionResult Browse(int page = 1)
        {
            var trips = this.Data.PublicTrips.All()
                            .OrderBy(x => x.Id)                            
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize)
                            .Project()
                            .To<PublicTripUserCardViewModel>()
                            .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.PublicTrips.All().Count() / PageSize);
            return View(trips);
        }

        [HttpGet]
        [Authorize]
        public ActionResult My(int page = 1)
        {
            var trips = this.Data.PublicTrips.All()
                            .Where(x => x.HostId == this.CurrentUser.Id)
                            .OrderBy(x => x.Id)                            
                            .Skip((page - 1) * PageSize)
                            .Take(PageSize)
                            .Project()
                            .To<PublicTripViewModel>()
                            .ToList();

            ViewBag.Pages = Math.Ceiling((double)this.Data.PublicTrips.All().Count() / PageSize);
            return View(trips);
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
        public ActionResult Delete(int id)
        {
            var owner = this.Data.PublicTrips.All().Where(tr => tr.Id == id).Select(tr => tr.HostId).FirstOrDefault();

            if (owner == null)
            {
                return HttpNotFound("Invalid trip Id");
            }

            if (owner != this.CurrentUser.Id)
            {
                return new HttpUnauthorizedResult("You can delete only trips that were initiated by you");
            }

            this.Data.PublicTrips.Delete(id);
            this.Data.SaveChanges();

            TempData[GlobalConstants.SuccessMessage] = "Successfully deleted a trip!";
            return RedirectToAction("My");
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