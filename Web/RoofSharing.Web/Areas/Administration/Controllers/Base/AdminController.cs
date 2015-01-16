namespace RoofSharing.Web.Areas.Administration.Controllers.Base
{
    using System.Web.Mvc;
    using RoofSharing.Common;
    using RoofSharing.Data;
    using RoofSharing.Web.Controllers;
    using Roofsharing.Services.Notifiers;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IRoofSharingData data, INotifierService notifier) : base(data, notifier)
        {
        }


    }
}