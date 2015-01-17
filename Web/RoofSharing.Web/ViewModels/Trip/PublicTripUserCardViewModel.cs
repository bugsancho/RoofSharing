namespace RoofSharing.Web.ViewModels.Trip
{
    using System;
    using System.Linq;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;
    using AutoMapper;

    public class PublicTripUserCardViewModel : IMapFrom<PublicTrip>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string HostId { get; set; }

        public string PictureUrl { get; set; }

        public string Names { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }
        
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public int TravellersCount { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<PublicTrip, PublicTripUserCardViewModel>()
                         .ForMember(m => m.Names, opt => opt.MapFrom(t => t.Host.FirstName + " " + t.Host.LastName))
                         .ForMember(m => m.PictureUrl, opt => opt.MapFrom(t => t.Host.PictureUrl))
                         .ForMember(m => m.TravellersCount, opt => opt.MapFrom(t => t.Participants.Count() + 1));
        }
    }
}