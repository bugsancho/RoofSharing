using RoofSharing.Data;
using RoofSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace RoofSharing.Web.Controllers.Api
{
    public class BaseApiController : ApiController
    {
        protected IRoofSharingData Data { get; set; }

        protected User CurrentUser { get; set; }
        
        public BaseApiController(IRoofSharingData data)
        {
            this.Data = data;
        }
    }
}