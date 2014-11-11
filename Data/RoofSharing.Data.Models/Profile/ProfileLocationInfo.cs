using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models.Profile
{
    public class ProfileLocationInfo
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public string LocationName { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public virtual User User { get; set; }
    }
}