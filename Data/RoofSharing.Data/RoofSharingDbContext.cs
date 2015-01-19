﻿namespace RoofSharing.Data
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using RoofSharing.Data.Migrations;
    using RoofSharing.Data.Models;
    using RoofSharing.Data.Models.Profile;

    public class RoofSharingDbContext : IdentityDbContext<User>
    {
        public RoofSharingDbContext() : base("DefaultConnection", throwIfV1Schema: false)
        {
            //Database.SetInitializer(new DropCreateDatabaseAlways<RoofSharingDbContext>());
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<RoofSharingDbContext, Configuration>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                        .HasOptional(f => f.LocationInfo)
                        .WithRequired(s => s.User)
                        .WillCascadeOnDelete(true);
            modelBuilder.Entity<User>()
                        .HasOptional(f => f.HousingInfo)
                        .WithRequired(s => s.User)
                        .WillCascadeOnDelete(true);
            modelBuilder.Entity<User>()
                        .HasOptional(f => f.PersonalityInfo)
                        .WithRequired(s => s.User)
                        .WillCascadeOnDelete(true);

            modelBuilder.Entity<Friendship>()
                        .HasRequired(c => c.FromUser)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<Friendship>()
                        .HasRequired(s => s.ToUser)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<HostInvitation>()
                        .HasRequired(s => s.Host)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            modelBuilder.Entity<HostInvitation>()
                        .HasRequired(s => s.Guest)
                        .WithMany()
                        .WillCascadeOnDelete(false);

            base.OnModelCreating(modelBuilder);
        }

        public static RoofSharingDbContext Create()
        {
            return new RoofSharingDbContext();
        }

        public IDbSet<PersonalityInfo> Personalities { get; set; }

        public IDbSet<LocationInfo> UserLocations { get; set; }

        public IDbSet<Friendship> Friendships { get; set; }

        public IDbSet<UserHousingInfo> Houses { get; set; }

        public IDbSet<HostInvitation> Invitations { get; set; }

        public IDbSet<PublicTrip> PublicTrips { get; set; }
    }
}