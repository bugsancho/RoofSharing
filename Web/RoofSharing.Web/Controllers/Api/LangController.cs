using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RoofSharing.Web.Controllers.Api
{
    public class LangController : ApiController
    {
        // GET: api/Lang
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Lang/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Lang
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Lang/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Lang/5
        public void Delete(int id)
        {
        }
    }
}
