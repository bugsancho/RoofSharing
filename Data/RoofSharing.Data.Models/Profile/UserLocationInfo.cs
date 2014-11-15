using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models.Profile
{
    public class UserLocationInfo
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public string Address { get; set; }

        public string AdditionalInfo { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public virtual User User { get; set; }
    }
}