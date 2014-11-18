using AutoMapper;
using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.Areas.Administration.ViewModels
{
    public class UserHousingAdminViewModel : BaseUserAdminViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        [Display(Name = "Preferred gender")]
        public Gender? PreferredGender { get; set; }

        [Display(Name = "Smoking Allowed?")]
        [UIHint("BoolNullable")]
        public bool? IsSmokingAllowed { get; set; }

        [Display(Name = "Pets allowed?")]
        [UIHint("BoolNullable")]
        public bool? ArePetsAllowed { get; set; }

        [Display(Name = "Has pets?")]
        [UIHint("BoolNullable")]
        public bool? HavePets { get; set; }

        [Display(Name = "Are rooms shared?")]
        [UIHint("BoolNullable")]
        public bool? AreRoomsShared { get; set; }

        [Display(Name = "Wheelchair accessible?")]
        [UIHint("BoolNullable")]
        public bool? WheelChairAccessible { get; set; }
        
        [Display(Name = "Available Beds")]
        public int? NumberOfAvailableBeds { get; set; }

        [Display(Name = "Additional Info")]
        [UIHint("MultiLineText")]
        public string AdditionalInfo { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserHousingAdminViewModel>()
                         .ForMember(m => m.PreferredGender, opt => opt.MapFrom(t => t.HousingInfo.PreferredGender))
                         .ForMember(m => m.IsSmokingAllowed, opt => opt.MapFrom(t => t.HousingInfo.IsSmokingAllowed))
                         .ForMember(m => m.ArePetsAllowed, opt => opt.MapFrom(t => t.HousingInfo.ArePetsAllowed))
                         .ForMember(m => m.HavePets, opt => opt.MapFrom(t => t.HousingInfo.HavePets))
                         .ForMember(m => m.AreRoomsShared, opt => opt.MapFrom(t => t.HousingInfo.AreRoomsShared))
                         .ForMember(m => m.WheelChairAccessible, opt => opt.MapFrom(t => t.HousingInfo.WheelChairAccessible))
                         .ForMember(m => m.NumberOfAvailableBeds, opt => opt.MapFrom(t => t.HousingInfo.NumberOfAvailableBeds))
                         .ForMember(m => m.AdditionalInfo, opt => opt.MapFrom(t => t.HousingInfo.AdditionalInfo));
            configuration.CreateMap<UserHousingInfo, UserHousingAdminViewModel>().ReverseMap();
            //configuration.CreateMap<UserAdminViewModel, User>()
            //             .ForMember(m => m.LocationInfo, opt => opt.MapFrom(t => new LocationInfo { City = t.City}));
        }
    }
}