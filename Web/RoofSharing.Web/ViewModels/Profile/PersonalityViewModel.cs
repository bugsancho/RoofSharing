using AutoMapper;
using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Web.Infrastructure.Mappings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.ViewModels.Profile
{
    public class PersonalityViewModel : IMapFrom<PersonalityInfo>, IHaveCustomMappings
    {
        public Gender? Gender { get; set; }
        
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2000")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("ObjectWithDefaultValue")]
        public string Hobbies { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("ObjectWithDefaultValue")]
        public string Interests { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("ObjectWithDefaultValue")]
        public string Education { get; set; }

        [UIHint("MultiLineText")]
        [UIHint("ObjectWithDefaultValue")]
        public string Occupation { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PersonalityInfo, PersonalityViewModel>()
                         .ForMember(m => m.FirstName, opt => opt.MapFrom(t => t.User.FirstName))
                         .ForMember(m => m.LastName, opt => opt.MapFrom(t => t.User.LastName));
        }
    }
}