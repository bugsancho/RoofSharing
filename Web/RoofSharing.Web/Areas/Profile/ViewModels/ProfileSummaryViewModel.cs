using AutoMapper;
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
    public class ProfileSummaryViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        [UIHint("StringWithDefaultValue")]
        [Display(Name="First Name")]
        public string FirstName { get; set; }
         
        [UIHint("StringWithDefaultValue")]
        [Display(Name="Last Name")]
        public string LastName { get; set; }

        [UIHint("StringWithDefaultValue")]
        public string Address { get; set; }

        [UIHint("StringWithDefaultValue")]
        [Display(Name="Phone Number")]
        public string PhoneNumber { get; set; }

        [UIHint("StringWithDefaultValue")]
        public string Email { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, ProfileSummaryViewModel>()
                         .ForMember(m => m.Address, opt => opt.MapFrom(t => t.LocationInfo.Address));
        }
    }
}