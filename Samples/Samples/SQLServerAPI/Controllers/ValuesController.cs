using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SQLServerAPI.Controllers
{
    public class ValuesController : ApiController
    {

        static List<string> _strings = new List<string>() { "sample1", "sample2" };
        // GET api/values
        public IEnumerable<string> Get()
        {
            return _strings;
        }

        // GET api/values/5
        public string Get(int id)
        {
            return _strings[id];
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
            _strings.Add(value);
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
            _strings[id] = value;
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
            _strings.RemoveAt(id);
        }
    }
}
