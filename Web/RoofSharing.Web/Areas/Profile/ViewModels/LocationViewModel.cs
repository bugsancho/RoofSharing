using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.Areas.Profile.ViewModels
{
    public class LocationViewModel : IMapFrom<ProfileLocationInfo>
    {
        [Required]
        [Display(Name = "Location")]
        public string LocationName { get; set; }

        [Required]
        public string Longitude { get; set; }

        [Required]
        public string Latitude { get; set; }
    }
}