namespace RoofSharing.Web.ViewModels.Host
{
    using System;
    using System.Linq;
    using AutoMapper;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;

    public class UserCardViewModel : IMapFrom<User>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Names { get; set; }

        public string PictureUrl { get; set; }
        
        public string Location { get; set; }
        
        public string Tagline { get; set; }

        public AcceptingGuestsStatusType AcceptingGuestsStatus { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<User, UserCardViewModel>()
                         .ForMember(m => m.Names, opt => opt.MapFrom(t => t.FirstName + " " + t.LastName))
                         .ForMember(m => m.Location, opt => opt.MapFrom(t => t.LocationInfo.City + ", " + t.LocationInfo.Country))
                         .ForMember(m => m.Tagline, opt => opt.MapFrom(t => t.PersonalityInfo.Tagline))
                         .ForMember(m => m.AcceptingGuestsStatus, opt => opt.MapFrom(t => t.HousingInfo.AcceptingGuestsStatus));
        }
    }
}