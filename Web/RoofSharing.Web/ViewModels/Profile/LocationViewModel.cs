using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RoofSharing.Web.ViewModels.Profile
{
    public class LocationViewModel : IMapFrom<LocationInfo>
    {
        [Required]
        [Display(Name = "Address")]
        [UIHint("ObjectWithDefaultValue")]
        public string Address { get; set; }

        [UIHint("ObjectWithDefaultValue")]
        public string Country { get; set; }

        [UIHint("ObjectWithDefaultValue")]
        public string City { get; set; }

        [Required]
        [UIHint("ObjectWithDefaultValue")]
        [ScaffoldColumn(false)]
        public string Longitude { get; set; }

        [Required]
        [UIHint("ObjectWithDefaultValue")]
        [ScaffoldColumn(false)]
        public string Latitude { get; set; }

        [Display(Name = "Tell us more about the location of your house")]
        [UIHint("MultiLineText")]
        [UIHint("ObjectWithDefaultValue")]
        public string AdditionalInfo { get; set; }
    }
}