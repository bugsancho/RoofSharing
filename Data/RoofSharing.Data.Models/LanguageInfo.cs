using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RoofSharing.Data.Models
{
    public class LanguageInfo
    {
        public int Id { get; set; }

        public Language Language { get; set; }

        public ProficiencyLevel ProficiencyLevel { get; set; }
    }
}