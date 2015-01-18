namespace RoofSharing.Web.Controllers.Base
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using RoofSharing.Data;
    using RoofSharing.Data.Models;
    using Roofsharing.Services.Notifiers;
    
    public class BaseController : Controller
    {
        protected IRoofSharingData Data { get; set; }

        public INotifierService Notifier { get; private set; }

        protected User CurrentUser { get; set; }
        
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