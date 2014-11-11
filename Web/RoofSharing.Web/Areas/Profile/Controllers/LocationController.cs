﻿using RoofSharing.Data;
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

namespace RoofSharing.Web.Areas.Profile.Controllers
{
    [Authorize]
    public class LocationController : BaseController
    {
        public LocationController(IRoofSharingData data) : base(data)
        {
        }

        // GET: Profile/Location
        public ActionResult Index()
        {
            var location = Mapper.Map<LocationViewModel>(this.CurrentUser.LocationInfo);
            return View(location);
        }
        
        public ActionResult Edit()
        {
            var location = Mapper.Map<LocationViewModel>(this.CurrentUser.LocationInfo);
            return View(location);
        }

        [HttpPost]
        public ActionResult Edit(LocationViewModel locationInfo)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var location = Mapper.Map<ProfileLocationInfo>(locationInfo);
                    this.CurrentUser.LocationInfo = location;
                    this.Data.SaveChanges();

                    return RedirectToAction("Index");
                }
                catch
                {
                    return View(locationInfo);
                }
            }

            return View(locationInfo);
        }
    }
}