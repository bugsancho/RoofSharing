namespace RoofSharing.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class HostInvitation
    {
        public int Id { get; set; }

        public string HostId { get; set; }

        public virtual User Host { get; set; }

        public string GuestId { get; set; }

        public virtual User Guest { get; set; }

        public string Message { get; set; }

        [DataType(DataType.Date)]        
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
        
        public InvitationStatusType Status { get; set; }
        
        [Range(0, 20, ErrorMessage = "You cannot plan a trip with more than {2} people")]
        public int NumberOfCompanions { get; set; }
    }
}