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
    public class UserPersonalityAdminViewModel : BaseUserAdminViewModel, IMapFrom<User>, IHaveCustomMappings
    {
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        public DateTime? BirthDate { get; set; }
        
        [UIHint("MultiLineText")]
        [UIHint("StringWithDefaultValue")]
        public string Hobbies { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("StringWithDefaultValue")]
        public string Interests { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("StringWithDefaultValue")]
        public string Education { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("StringWithDefaultValue")]
        public string Occupation { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserPersonalityAdminViewModel>()
                         .ForMember(m => m.BirthDate, opt => opt.MapFrom(t => t.PersonalityInfo.BirthDate))
                         .ForMember(m => m.Education, opt => opt.MapFrom(t => t.PersonalityInfo.Education))
                         .ForMember(m => m.Occupation, opt => opt.MapFrom(t => t.PersonalityInfo.Occupation))
                         .ForMember(m => m.Interests, opt => opt.MapFrom(t => t.PersonalityInfo.Interests))
                         .ForMember(m => m.Hobbies, opt => opt.MapFrom(t => t.PersonalityInfo.Hobbies));
            ;
            configuration.CreateMap<PersonalityInfo, UserPersonalityAdminViewModel>().ReverseMap();
        }
    }
}