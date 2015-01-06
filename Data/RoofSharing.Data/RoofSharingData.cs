using RoofSharing.Data.Models;
using RoofSharing.Data.Models.Profile;
using RoofSharing.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoofSharing.Data
{
    public class RoofSharingData : IRoofSharingData
    {
        public DbContext Context { get; private set; }

        private IDictionary<Type, object> repositories;

        public RoofSharingData(DbContext context)
        {
            this.Context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public IRepository<User> Users
        
        {
            get
            {
                return this.GetRepository<User>();
            }
        }

        public IRepository<Language> Languages
        
        {
            get
            {
                return this.GetRepository<Language>();
            }
        }

         public IRepository<Friendship> Friendships
        
        {
            get
            {
                return this.GetRepository<Friendship>();
            }
        }

        public IRepository<PersonalityInfo> Personalities
        
        {
            get
            {
                return this.GetRepository<PersonalityInfo>();
            }
        }

           public IRepository<HostInvitation> Invitations
        
        {
            get
            {
                return this.GetRepository<HostInvitation>();
            }
        }

        public int SaveChanges()
        {
            return this.Context.SaveChanges();
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(EntityFrameworkRepository<T>), Context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }
    }
}