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
    public class HousingViewModel : IMapFrom<UserHousingInfo>
    {
        public PreferredGender PreferredGender { get; set; }

        [Display(Name = "Is Smoking Allowed?")]
        [UIHint("BoolNullable")]
        public bool? IsSmokingAllowed { get; set; }

        [Display(Name = "Are pets allowed?")]
        [UIHint("BoolNullable")]
        public bool? ArePetsAllowed { get; set; }

        [Display(Name = "Do you have pets?")]
        [UIHint("BoolNullable")]
        public bool? HavePets { get; set; }

        [Display(Name = "Are rooms shared?")]
        [UIHint("BoolNullable")]
        public bool? AreRoomsShared { get; set; }

        [Display(Name = "Is your place wheelchair accessible?")]
        [UIHint("BoolNullable")]
        public bool? WheelChairAccessible { get; set; }
        
        [Display(Name = "How many available beds are there?")]
        public int? NumberOfAvailableBeds { get; set; }

        [Display(Name = "Tell us more about your house")]
        [UIHint("MultiLineText")]
        public string AdditionalInfo { get; set; }
    }
}