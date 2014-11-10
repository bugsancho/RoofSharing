using RoofSharing.Data;
using RoofSharing.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoofSharing.Web.Controllers.Api
{
    public class LanguagesController : BaseApiController
    {
        public LanguagesController() : this(new RoofSharingData(new RoofSharingDbContext()))
        {
        }

        public LanguagesController(IRoofSharingData data) : base(data)
        {
        }

        [HttpGet]
        public IQueryable<Language> GetAllLanguages(string query = null)
        {
            if (query != null)
            {
                return this.Data.Languages.All().Where(l => l.EnglishName.StartsWith(query));
            }
            return this.Data.Languages.All();
        }

        [HttpPost]
        public void CreateLanguages(IEnumerable<Language> languages)
        {
            foreach (var lang in languages)
            {
                this.Data.Languages.Add(lang);
            }
            this.Data.SaveChanges();
        }
    }
}