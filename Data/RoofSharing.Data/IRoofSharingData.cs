using System;
using System.Collections.Generic;
using System.Data.Entity;
using RoofSharing.Data.Models;
using RoofSharing.Data.Repositories;
using RoofSharing.Data.Models.Profile;

namespace RoofSharing.Data
{
    public interface IRoofSharingData
    {
        IRepository<User> Users { get; }

        IRepository<Language> Languages { get; }

        IRepository<Friendship> Friendships { get; }

        IRepository<PersonalityInfo> Personalities { get; }

        IRepository<HostInvitation> Invitations { get; }

        IRepository<PublicTrip> PublicTrips { get; }

        DbContext Context { get; }

        int SaveChanges();
    }
}