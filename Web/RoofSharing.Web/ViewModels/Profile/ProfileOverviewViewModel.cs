namespace RoofSharing.Web.ViewModels.Profile
{
    using System;
    using System.Linq;
    using AutoMapper;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;

    public class ProfileOverviewViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string Location { get; set; }

        public string Gender { get; set; }

        public int? Age { get; set; }

        public string Tagline { get; set; }

        public string PictureUrl { get; set; }

        public string Occupation { get; set; }

        public string Education { get; set; }

        public string Hobby { get; set; }
        
        public string Languages { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            //TODO Refactor age calculation
            configuration.CreateMap<User, ProfileOverviewViewModel>()
                         .ForMember(m => m.Names, opt => opt.MapFrom(t => t.FirstName + " " + t.LastName))
                         .ForMember(m => m.Location, opt => opt.MapFrom(t => t.LocationInfo.City + ", " + t.LocationInfo.Country))
                         .ForMember(m => m.Age, opt => opt.MapFrom(t => (DateTime.Now.Year - t.PersonalityInfo.BirthDate.Value.Year) -
                                                                        ((DateTime.Now.Month < t.PersonalityInfo.BirthDate.Value.Month) ||
                                                                         ((DateTime.Now.Month == t.PersonalityInfo.BirthDate.Value.Month) &&
                                                                          (DateTime.Now.Day < t.PersonalityInfo.BirthDate.Value.Day)) ? 1 : 0)))
                         .ForMember(m => m.Tagline, opt => opt.MapFrom(t => t.PersonalityInfo.Tagline))
                         .ForMember(m => m.Occupation, opt => opt.MapFrom(t => t.PersonalityInfo.Occupation))
                         .ForMember(m => m.Education, opt => opt.MapFrom(t => t.PersonalityInfo.Education))
                         .ForMember(m => m.Hobby, opt => opt.MapFrom(t => t.PersonalityInfo.Hobbies))
                         .ForMember(m => m.Languages, opt => opt.MapFrom(t => t.PersonalityInfo.Languages));
        }
    }
}