namespace RoofSharing.Web.ViewModels.Trip
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;
    using RoofSharing.Web.Infrastructure.ValidationAttributes;
    using RoofSharing.Web.ViewModels.Account;
    using System.Web.Mvc;
    
    public class PublicTripViewModel : IMapFrom<PublicTrip>
    {
        [HiddenInput]
        public int Id { get; set; }
        
        [HiddenInput]
        public string HostId { get; set; }

        [Required]
        [Display(Name = "Starting Point")]
        public string StartPoint { get; set; }

        [Required]
        [HiddenInput]
        public string StartPointCity { get; set; }

        [Required]
        [Display(Name = "Destination")]
        public string EndPoint { get; set; }

        [Required]
        [HiddenInput]
        public string EndPointCity { get; set; }
        
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