using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.Areas.Administration.ViewModels
{
    public class BaseUserAdminViewModel
    {
         [Editable(false)]
        public string Id { get; set; }
        
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [UIHint("ObjectWithDefaultValue")]
        public string FirstName { get; set; }
         
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [UIHint("ObjectWithDefaultValue")]
        public string LastName { get; set; }
    }
}