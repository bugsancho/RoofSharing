using RoofSharing.Data;
using RoofSharing.Web.Areas.Administration.Controllers.Base;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using RoofSharing.Web.Areas.Administration.ViewModels;
using Kendo.Mvc.UI;
using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using Roofsharing.Services.Notifiers;

namespace RoofSharing.Web.Areas.Administration.Controllers
{
     public class UsersHousingController : KendoGridAdminController
    {

        public UsersHousingController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }

        protected override T GetById<T>(object id)
        {
            return this.Data.Users.Find(id) as T;
        }

        protected override IEnumerable GetData()
        {
            return this.Data
            .Users
                       .All()
                       .Project()
                       .To<UserHousingAdminViewModel>();
        }

        [HttpPost]
        public ActionResult Create([DataSourceRequest]
                                   DataSourceRequest request, UserHousingAdminViewModel model)
        {
            var dbModel = base.Create<User>(model);
            if (dbModel != null)
                model.Id = dbModel.Id;
            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Update([DataSourceRequest]
                                   DataSourceRequest request, UserHousingAdminViewModel model)
        {
            var user = this.Data.Users.Find(model.Id);
            user = Mapper.Map<UserHousingAdminViewModel, User>(model, user);
            user.HousingInfo = Mapper.Map<UserHousingAdminViewModel, UserHousingInfo>(model, user.HousingInfo);
            this.Data.SaveChanges();

            return this.GridOperation(model, request);
        }

        [HttpPost]
        public ActionResult Destroy([DataSourceRequest]
                                    DataSourceRequest request, UserHousingAdminViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var user = this.Data.Users.Find(model.Id);

                this.Data.Users.Delete(user);
                
                this.Data.SaveChanges();
            }

            return this.GridOperation(model, request);
        }

        // GET: Administration/Users
        public ActionResult Index()
        {
            return View();
        }
    }
}