namespace RoofSharing.Web.ViewModels.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using RoofSharing.Data.Models;
    using RoofSharing.Data.Models.Profile;
    using RoofSharing.Web.Infrastructure.Mappings;

    public class HousingViewModel : IMapFrom<UserHousingInfo>
    {
        [Display(Name="Guest status")]
        public AcceptingGuestsStatusType AcceptingGuestsStatus { get; set; }

        [Display(Name = "Preferred traveller gender")]        
        [UIHint("ObjectWithDefaultValue")]
        public Gender? PreferredGender { get; set; }

        [Display(Name = "Is Smoking Allowed?")]
        [UIHint("BoolNullable")]
        public bool? IsSmokingAllowed { get; set; }

        [Display(Name = "Are pets allowed?")]
        [UIHint("BoolNullable")]
        public bool? ArePetsAllowed { get; set; }
        
        [Display(Name = "Have pets?")]
        [UIHint("BoolNullable")]
        public bool? HavePets { get; set; }

        [Display(Name = "Are rooms shared?")]
        [UIHint("BoolNullable")]
        public bool? AreRoomsShared { get; set; }

        [Display(Name = "Is wheelchair accessible?")]
        [UIHint("BoolNullable")]
        public bool? WheelChairAccessible { get; set; }
        
        [Display(Name = "Available beds")]
        [UIHint("ObjectWithDefaultValue")]
        public int? NumberOfAvailableBeds { get; set; }

        [Display(Name = "Additional Info")]
        [UIHint("MultiLineText")]
        public string AdditionalInfo { get; set; }
    }
}