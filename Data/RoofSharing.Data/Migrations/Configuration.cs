namespace RoofSharing.Data.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<RoofSharing.Data.RoofSharingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
            ContextKey = "RoofSharing.Data.RoofSharingDbContext";
        }

        protected override void Seed(RoofSharing.Data.RoofSharingDbContext context)
        {
        }
    }
}