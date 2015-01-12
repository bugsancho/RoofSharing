﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RoofSharing.Data;
using RoofSharing.Web.ViewModels.Trip;
using AutoMapper.QueryableExtensions;

namespace RoofSharing.Web.Controllers
{
    public class TripsController : BaseController
    {
        private const string PlannedTripViewModelName = "plannedTripViewModel";

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
                TempData[PlannedTripViewModelName] = model;
                return RedirectToAction("Matches");
            }
            return View(model);
        }
        
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
    }
}