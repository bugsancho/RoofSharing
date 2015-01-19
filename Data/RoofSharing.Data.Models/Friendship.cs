namespace RoofSharing.Data.Models
{
    using System.ComponentModel.DataAnnotations;

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