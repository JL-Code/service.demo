using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AuthorizationServer.Api.Entities
{
    /// <summary>
    /// JWT的受众
    /// </summary>
    public class Audience
    {
        public string ClientId { get; set; }

        public string Base64Secret { get; set; }

        public string Name { get; set; }
    }
}