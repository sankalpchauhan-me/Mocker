using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ContentNegotiationAndMediaFormatter.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {

            var strings = new string[] { "value1", "value2" };
            //IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            //ContentNegotiationResult result = negotiator.Negotiate(typeof(string), this.Request, this.Configuration.Formatters);

            //if(result == null)
            //{
            //    var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            //    throw new HttpResponseException(response);
            //}
            return strings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
