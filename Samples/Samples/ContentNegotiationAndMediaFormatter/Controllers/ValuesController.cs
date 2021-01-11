using ContentNegotiationAndMediaFormatter.Models;
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
        List<Product> _productList = new List<Product>()
        {
            new Product(101, "P1", "C1", 299),
            new Product(102, "P2", "C1", 100),
            new Product(103, "P3", "C2", 21),
            new Product(104, "P4", "C2", 111)
        };
        // GET api/values
        public IEnumerable<Product> Get()
        {
            //IContentNegotiator negotiator = this.Configuration.Services.GetContentNegotiator();
            //ContentNegotiationResult result = negotiator.Negotiate(typeof(string), this.Request, this.Configuration.Formatters);

            //if(result == null)
            //{
            //    var response = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            //    throw new HttpResponseException(response);
            //}
            return _productList;
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
