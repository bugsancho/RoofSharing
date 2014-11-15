using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.Areas.Profile.ViewModels
{
    public class LocationViewModel : IMapFrom<UserLocationInfo>
    {
        [Required]
        [Display(Name = "Address")]
        public string Address { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }

        [Display(Name = "Tell us more about your place")]
        [UIHint("MultiLineText")]
        public string AdditionalInfo { get; set; }
    }
}