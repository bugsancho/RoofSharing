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
        [HiddenInput(DisplayValue = false)]
        public string HostId { get; set; }
        
        [Display(Name = "Host Names")]
        [Editable(false)]
        public string HostNames { get; set; }

        [DataType(DataType.Date)]
        [DateTimeCombinedValidation("EndDate")]
        [Display(Name = "Start Date")]
        [Required]
        public DateTime? StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "End Date")]
        [Required]
        public DateTime? EndDate { get; set; }

        [Display(Name = "How many people will you be traveling with?")]
        [Range(0, 20, ErrorMessage = "You cannot plan a trip with more than {2} people")]
        public int NumberOfCompanions { get; set; }

        [Required]
        [StringLength(1000)]
        [UIHint("MultiLineText")]
        public string Message { get; set; }

        public void CreateMappings(IConfiguration configuration)
        {
            configuration.CreateMap<HostInvitation, HostInviteViewModel>()
                         .ForMember(m => m.HostNames, opt => opt.MapFrom(t => t.Host.FirstName + " " + t.Host.LastName));
        }
    }
}