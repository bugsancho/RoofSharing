namespace RoofSharing.Data.Models.Profile
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using RoofSharing.Data.Models;

    public class UserHousingInfo
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public AcceptingGuestsStatusType AcceptingGuestsStatus { get; set; }

        public Gender? PreferredGender { get; set; }
        
        public bool? IsSmokingAllowed { get; set; }

        public bool? ArePetsAllowed { get; set; }

        public bool? HavePets { get; set; }

        public bool? AreRoomsShared { get; set; }

        public bool? WheelChairAccessible { get; set; }

        public int? NumberOfAvailableBeds { get; set; }

        public string AdditionalInfo { get; set; }

        public virtual User User { get; set; }
    }
}