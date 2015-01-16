using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models
{
    public enum AcceptingGuestsStatusType
    {
        [Display(Name = "Maybe Accepting Guests")]
        [Description("asd asd asd")]
        MaybeAcceptingGuests,
        [Display(Name = "Accepting Guests")]
        AcceptingGuests,       
        [Display(Name = "Not Accepting Guests")]
        NotAcceptingGuests
    }
}