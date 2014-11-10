using Microsoft.AspNet.Identity.EntityFramework;
using RoofSharing.Data.Models;
using System.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoofSharing.Data.Migrations;

namespace RoofSharing.Data
{
    public class RoofSharingDbContext : IdentityDbContext<User>
    {
        public RoofSharingDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RoofSharingDbContext, Configuration>());
        }

        public static RoofSharingDbContext Create()
        {
            return new RoofSharingDbContext();
        }
    }
}   