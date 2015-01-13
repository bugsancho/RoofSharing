﻿using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Profile
{
    public class HousingViewModel : IMapFrom<UserHousingInfo>
    {
        [Display(Name="Preferred traveller gender")]
        public Gender? PreferredGender { get; set; }

        [Display(Name = "Is Smoking Allowed?")]
        [UIHint("BoolNullable")]
        [UIHint("ObjectWithDefaultValue")]
        public bool? IsSmokingAllowed { get; set; }

        [Display(Name = "Are pets allowed?")]
        [UIHint("BoolNullable")]
        [UIHint("ObjectWithDefaultValue")]
        public bool? ArePetsAllowed { get; set; }

        [Display(Name = "Have pets?")]
        [UIHint("BoolNullable")]
        public bool? HavePets { get; set; }

        [Display(Name = "Are rooms shared?")]
        [UIHint("BoolNullable")]
        [UIHint("ObjectWithDefaultValue")]
        public bool? AreRoomsShared { get; set; }

        [Display(Name = "Is wheelchair accessible?")]
        [UIHint("BoolNullable")]
        [UIHint("ObjectWithDefaultValue")]
        public bool? WheelChairAccessible { get; set; }
        
        [Display(Name = "Available beds")]
        public int? NumberOfAvailableBeds { get; set; }

        [Display(Name = "Additional Info")]
        [UIHint("MultiLineText")]
        public string AdditionalInfo { get; set; }
    }
}