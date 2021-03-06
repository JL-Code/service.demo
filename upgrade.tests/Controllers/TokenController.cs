﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace upgrade.tests.Controllers
{
    /// <summary>
    /// Token签发
    /// </summary>
    public class TokenController : ApiController
    {
        // GET: api/Token
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Token/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Token
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Token/5
        public void Put(int id, [FromBody]string value)
        {
            var identity = User.Identity as ClaimsIdentity;

            var info = identity.Claims.Select(c => new
            {
                Type = c.Type,
                Value = c.Value
            });
        }

        // DELETE: api/Token/5
        public void Delete(int id)
        {
        }
    }
}
