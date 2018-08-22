using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClientCredentialApi.Controllers
{
    [Produces("application/json")]
    [Route("Home")]
    [Authorize]
    public class HomeController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Home1", "Home2" };
        }
    }
}