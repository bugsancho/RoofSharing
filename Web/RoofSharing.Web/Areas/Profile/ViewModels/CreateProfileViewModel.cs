using RoofSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RoofSharing.Web.Areas.Profile.ViewModels
{
    public class CreateProfileViewModel
    {
        public ICollection<LanguageInfo> Languages { get; set; }
    }
}