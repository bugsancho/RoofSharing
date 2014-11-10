using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace RoofSharing.Data.Models
{
    public enum ProficiencyLevel
    {
        [Display(Name="Basic Speaker")]
        BasicSpeaker,
        Intermediate,
        Proficient,
        NativeSpeaker
    }
}