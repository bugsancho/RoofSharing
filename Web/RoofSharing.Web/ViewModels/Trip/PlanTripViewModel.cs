using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using RoofSharing.Web.Infrastructure.ValidationAttributes;

namespace RoofSharing.Web.ViewModels.Trip
{
    public class PlanTripViewModel
    {
        [Required]
        public string City { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd", ApplyFormatInEditMode = true)]
        [DateTimeCombinedValidation("EndDate")]
        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }
        
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "yyyy-MM-dd", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }
        
        [Display(Name = "How many people will you be traveling with?")]
        [Range(0, 20, ErrorMessage = "You cannot plan a trip with more than {2} people")]
        public int NumberOfCompanions { get; set; }
    }
}