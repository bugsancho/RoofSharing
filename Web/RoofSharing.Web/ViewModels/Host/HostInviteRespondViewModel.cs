namespace RoofSharing.Web.ViewModels.Host
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;

    public class HostInviteRespondViewModel : IMapFrom<HostInvitation>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Guest Names")]
        public string GuestNames { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]     
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]  
        public DateTime EndDate { get; set; }

        public string Message { get; set; }

        [Display(Name = "Number of companions")]
        public int NumberOfCompanions { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<HostInvitation, HostInviteRespondViewModel>()
                         .ForMember(m => m.GuestNames, opt => opt.MapFrom(t => t.Guest.FirstName + " " + t.Guest.LastName));
        }
    }
}