using DBLib.Models;
using Mocker.Filter;
using Mocker.Repository;
using Mocker.Utils;
using Mocker.ViewModels;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.MAIN_ROUTE_PREFIX)]
    public class MainController : ApiController
    {

        private MockerRepository _repository;

        public MainController()
        {
            _repository = new MockerRepository();
        }
        // GET api/values
        [Route(Constants.DEFAULT_ROUTE)]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<Developer> fulldata = _repository.GetAllInfo();
                List<DeveloperViewModel> developerViewModels = new List<DeveloperViewModel>();
                foreach (Developer d in fulldata)
                {
                    developerViewModels.Add(d);
                }
                return Ok(developerViewModels);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
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
