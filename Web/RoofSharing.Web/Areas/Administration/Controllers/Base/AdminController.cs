namespace RoofSharing.Web.Areas.Administration.Controllers.Base
{
    using RoofSharing.Common;
    using RoofSharing.Data;
    using RoofSharing.Web.Controllers;
    using System.Web.Mvc;

    [Authorize(Roles = GlobalConstants.AdminRoleName)]
    public abstract class AdminController : BaseController
    {
        public AdminController(IRoofSharingData data)
            : base(data)
        {
        }

    }
}