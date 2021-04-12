using System.Collections.Generic;
using System.Web.Http;

namespace WebService.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        [Authorize]
        [ActionName("v2")]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize]
        [ActionName("v2")]
        public string Get(int id)
        {
            return "value";
        }

        [Authorize]
        [ActionName("v2")]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [Authorize]
        [ActionName("v2")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [Authorize]
        [ActionName("v2")]
        public void Delete(int id)
        {
        }
    }
}
