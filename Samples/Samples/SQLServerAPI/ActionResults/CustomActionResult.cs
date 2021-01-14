using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Hosting;

namespace SQLServerAPI.ActionResultCustom
{
    public class CustomActionResult : IHttpActionResult
    {
        string _name;
        public CustomActionResult(string name)
        {
            _name = name;
        }
        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            return Task.FromResult(getMessage(_name));
        }

        public HttpResponseMessage getMessage(string name)
        {
            HttpRequestMessage request = new HttpRequestMessage();
            request.Properties.Add(HttpPropertyKeys.HttpConfigurationKey, new HttpConfiguration());
            HttpResponseMessage message = request.CreateResponse((HttpStatusCode)222, "You found the secret link " + name);
            return message;
        }
    }
}