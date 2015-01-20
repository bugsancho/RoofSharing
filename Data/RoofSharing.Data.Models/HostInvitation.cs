namespace RoofSharing.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HostInvitation
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public string HostId { get; set; }

        public virtual User Host { get; set; }

        [Required]
        public string GuestId { get; set; }

        public virtual User Guest { get; set; }

        [Required]
        public string Message { get; set; }

        [Required]
        [DataType(DataType.Date)]        
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        [Required]
        public InvitationStatusType Status { get; set; }
        
        [Range(0, 20, ErrorMessage = "You cannot plan a trip with more than {2} people")]
        [Required]
        public int NumberOfCompanions { get; set; }
    }
}