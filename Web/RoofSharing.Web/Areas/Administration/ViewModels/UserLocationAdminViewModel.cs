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
    public class UserLocationAdminViewModel : BaseUserAdminViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        public string Address { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserLocationAdminViewModel>()
                         .ForMember(m => m.City, opt => opt.MapFrom(t => t.LocationInfo.City))
                         .ForMember(m => m.Address, opt => opt.MapFrom(t => t.LocationInfo.Address))
                         .ForMember(m => m.Country, opt => opt.MapFrom(t => t.LocationInfo.Country));
            configuration.CreateMap<LocationInfo, UserLocationAdminViewModel>().ReverseMap();
            //configuration.CreateMap<UserAdminViewModel, User>()
            //             .ForMember(m => m.LocationInfo, opt => opt.MapFrom(t => new LocationInfo { City = t.City}));
        }
    }
}