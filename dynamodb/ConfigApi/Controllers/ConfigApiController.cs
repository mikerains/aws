using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ConfigApi.Helpers;

namespace ConfigApi.Controllers
{
    [RoutePrefix("ConfigApi")]
    public class ConfigApiController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get(string ProjectName, string ConfigKey)
        {
            ConfigService.ConfigManager mgr = new ConfigService.ConfigManager();
            var result = mgr.Get(ProjectName, ConfigKey);
            var response = this.JsonString(result.ToString()); //defaults to 200 OK

            return response;
        }
    }
}
