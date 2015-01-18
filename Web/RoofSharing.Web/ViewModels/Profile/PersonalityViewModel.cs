namespace RoofSharing.Web.ViewModels.Profile
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using RoofSharing.Data.Models;
    using RoofSharing.Data.Models.Profile;
    using RoofSharing.Web.Infrastructure.Mappings;

    public class PersonalityViewModel : IMapFrom<PersonalityInfo>, IHaveCustomMappings
    {
        public Gender? Gender { get; set; }
        
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Range(typeof(DateTime), "01/01/1900", "01/01/2000")]
        [UIHint("ObjectWithDefaultValue")]
        public DateTime? BirthDate { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, MinimumLength = 2)]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, MinimumLength = 2)]
        public string LastName { get; set; }

        [DataType(DataType.MultilineText)]
        public string Hobbies { get; set; }

        [DataType(DataType.MultilineText)]
        public string Interests { get; set; }

        [DataType(DataType.MultilineText)]
        public string Education { get; set; }

        [DataType(DataType.MultilineText)]
        public string Occupation { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PersonalityInfo, PersonalityViewModel>()
                         .ForMember(m => m.FirstName, opt => opt.MapFrom(t => t.User.FirstName))
                         .ForMember(m => m.LastName, opt => opt.MapFrom(t => t.User.LastName));
        }
    }
}