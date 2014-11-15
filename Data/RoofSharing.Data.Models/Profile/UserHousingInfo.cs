using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofSharing.Data.Models;

namespace RoofSharing.Data.Models.Profile
{
    public class UserHousingInfo
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public PreferredGender PreferredGender { get; set; }

        
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