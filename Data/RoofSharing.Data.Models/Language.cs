using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace RoofSharing.Data.Models
{
    public class Language
    {
        [StringLength(2, MinimumLength = 2)]
        [Key]
        public string Code { get; set; }

        public string EnglishName { get; set; }

        public string NativeName { get; set; }
    }
}