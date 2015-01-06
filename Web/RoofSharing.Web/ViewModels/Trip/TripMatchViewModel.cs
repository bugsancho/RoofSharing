using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Trip
{
    public class TripMatchViewModel
    {
        [Required]
        public string City { get; set; }

        public IEnumerable<string> Ids { get; set; }
    }
}