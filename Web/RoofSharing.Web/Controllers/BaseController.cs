using RoofSharing.Data;
using RoofSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Roofsharing.Services.Notifiers;

namespace RoofSharing.Web.Controllers
{
    public class BaseController : Controller
    {
        protected IRoofSharingData Data { get; set; }

        public INotifierService Notifier { get; private set; }

        protected User CurrentUser { get; set; }

        //public BaseController() : this(new RoofSharingData(new RoofSharingDbContext()))
        //{
        //}

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (requestContext.HttpContext.User.Identity.IsAuthenticated)
            {
                this.CurrentUser = Data.Users.Find(requestContext.HttpContext.User.Identity.GetUserId());
                if (this.CurrentUser != null)
                {
                    this.ViewData["DisplayName"] = CurrentUser.FirstName;
                }
            }
            base.Initialize(requestContext);
        }

        public BaseController(IRoofSharingData data, INotifierService notifier)
        {
            this.Data = data;
            this.Notifier = notifier;
        }
    }
}