using AutoMapper;
using RoofSharing.Data.Models;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Account
{
    public class UserViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string Location { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserViewModel>()
                         .ForMember(m => m.Names, opt => opt.MapFrom(t => t.FirstName + " " + t.LastName))
                         .ForMember(m => m.Location, opt => opt.MapFrom(t => t.LocationInfo.City + ", " + t.LocationInfo.Country));
        }
    }
}