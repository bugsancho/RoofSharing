using RoofSharing.Data.Models;
using RoofSharing.Web.Infrastructure.Mappings;
using RoofSharing.Web.Infrastructure.ValidationAttributes;
using RoofSharing.Web.ViewModels.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace RoofSharing.Web.ViewModels.Trip
{
    public class PublicTripViewModel : IMapFrom<PublicTrip>
    {
        public int Id { get; set; }
        
        public string HostId { get; set; }

        [Required]
        [Display(Name = "Starting Point")]
        public string StartPoint { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string EndPoint { get; set; }
        
        [DateTimeCombinedValidation("EndDate")]
        [Required]
        [Display(Name = "Departure Date")]
        public DateTime StartDate { get; set; }

        [Required]
        [Display(Name = "Return Date")]
        public DateTime EndDate { get; set; }
        
        [UIHint("MultiLineText")]
        [Required]
        [StringLength(500, MinimumLength = 10)]
        public string Description { get; set; }
        
        public ICollection<UserViewModel> Participants { get; set; }

        public PublicTripViewModel()
        {
            this.Participants = new HashSet<UserViewModel>();
        }
    }
}