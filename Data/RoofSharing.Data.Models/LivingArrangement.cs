using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models
{
    public enum LivingArrangement
    {
        
        Alone,
        [Display(Name="With Parents")]
        WithParents,
        [Display(Name="With Roommate")]
        WithRoommate,
        [Display(Name="With Partner")]
        WithPartner
    }
}
