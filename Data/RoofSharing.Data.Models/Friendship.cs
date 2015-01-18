using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models
{
    public class Friendship
    {
        public int Id { get; set; }

        public FriendshipStatusType Status { get; set; }

        [Required]
        public string FromUserId { get; set; }

        public virtual User FromUser { get; set; }
        
        [Required]        
        public string ToUserId { get; set; }
        
        public virtual User ToUser { get; set; }
    }
}