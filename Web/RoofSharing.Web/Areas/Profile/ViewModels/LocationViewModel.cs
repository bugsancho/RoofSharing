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
    public class LocationViewModel : IMapFrom<LocationInfo>
    {
        [Required]
        [Display(Name = "Address")]
        [UIHint("StringWithDefaultValue")]
        public string Address { get; set; }

        [UIHint("StringWithDefaultValue")]
        public string Country { get; set; }

        [UIHint("StringWithDefaultValue")]
        public string City { get; set; }

        [Required]
        [UIHint("StringWithDefaultValue")]
        public string Longitude { get; set; }

        [Required]
        [UIHint("StringWithDefaultValue")]
        public string Latitude { get; set; }

        [Display(Name = "Tell us more about your place")]
        [UIHint("MultiLineText")]
        [UIHint("StringWithDefaultValue")]
        public string AdditionalInfo { get; set; }
    }
}