using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Host
{
    public class HostInviteInputViewModel
    {

        [Required]
        public string UserId { get; set; }
    }
}