using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models.Profile
{
    public class PersonalityInfo
    {
        [Key,ForeignKey("User")]
        public string Id { get; set; }

        public Gender Gender { get; set; }
        
        [Range(typeof(DateTime), "01/01/1900", "01/01/2000")]
        public DateTime? BirthDate { get; set; }

        [StringLength(500)]
        public string Hobbies { get; set; }

        [StringLength(500)]
        public string Interests { get; set; }

        public string Tagline { get; set; }

        [StringLength(500)]
        public string Education { get; set; }

        [StringLength(500)]
        public string Occupation { get; set; }

        public string Languages { get; set; }
        
        public virtual User User { get; set; }
    }
}