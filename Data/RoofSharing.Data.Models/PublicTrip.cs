using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models
{
    public class PublicTrip
    {
        private ICollection<User> participants;

        public int Id { get; set; }

        public string HostId { get; set; }

        public User Host { get; set; }

        public string StartPoint { get; set; }

        public string EndPoint { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }
        
        public string Description { get; set; }
        
        public PublicTrip()
        {
            this.participants = new HashSet<User>();
        }

        public ICollection<User> Participants
        {
            get
            {
                return this.participants;
            }
            set
            {
                this.participants = value;
            }
        }
    }
}