using System;
using System.Collections.Generic;
using System.Data.Entity;
using RoofSharing.Data.Models;
using RoofSharing.Data.Repositories;

namespace RoofSharing.Data
{
    public interface IRoofSharingData
    {
        IRepository<User> Users { get; }

        int SaveChanges();
    }
}