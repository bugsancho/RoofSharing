using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using RoofSharing.Data.Models.Profile;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data.Models
{
    public class User : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [UIHint("ObjectWithDefaultValue")]
        public string FirstName { get; set; }
         
        [Required]
        [StringLength(50, MinimumLength = 2)]
        [UIHint("ObjectWithDefaultValue")]
        public string LastName { get; set; }

        [Required]
        public string PictureUrl { get; set; }

        public virtual LocationInfo LocationInfo { get; set; }

        public virtual UserHousingInfo HousingInfo { get; set; }

        public virtual PersonalityInfo PersonalityInfo { get; set; }
    }
}