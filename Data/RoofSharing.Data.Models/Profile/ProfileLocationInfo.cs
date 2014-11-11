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
        [Key,ForeignKey("UserId")]
        public string UserId { get; set; }
    }
}