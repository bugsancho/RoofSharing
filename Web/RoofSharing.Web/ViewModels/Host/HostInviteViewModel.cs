namespace RoofSharing.Web.ViewModels.Host
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using AutoMapper;
    using RoofSharing.Data.Models;
    using RoofSharing.Web.Infrastructure.Mappings;
    using RoofSharing.Web.Infrastructure.ValidationAttributes;
    using System.Web.Mvc;

    public class HostInviteViewModel : IMapFrom<HostInvitation>, IHaveCustomMappings
    {
        [Required]
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [ScaffoldColumn(false)]
        public string HostId { get; set; }
        
        [Display(Name = "Host Names")]
        [Editable(false)]
        public string HostNames { get; set; }
        
        [DateTimeCombinedValidation("EndDate")]
        [Display(Name = "Start Date")]
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")] 
        public DateTime StartDate { get; set; }
        
        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd MMMM yyyy}")]
        [Required]
        public DateTime EndDate { get; set; }

        [Display(Name = "How many people will you be traveling with?")]
        [Range(0, 20, ErrorMessage = "You cannot plan a trip with more than {2} people")]
        public int NumberOfCompanions { get; set; }

        [Editable(false)]
        public InvitationStatusType Status { get; set; }

        [Required]
        [StringLength(1000)]
        [DataType(DataType.MultilineText)]
        public string Message { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<HostInvitation, HostInviteViewModel>()
                         .ForMember(m => m.HostNames, opt => opt.MapFrom(t => t.Host.FirstName + " " + t.Host.LastName));
        }
    }
}