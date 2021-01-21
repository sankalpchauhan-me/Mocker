using DBLib.Models;
using Mocker.Filter;
using Mocker.Repository;
using Mocker.Utils;
using Mocker.DTOs;
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

        [Route(Constants.DEFAULT_ROUTE)]
        public IHttpActionResult GetAll()
        {
            try
            {
                List<Developer> fulldata = _repository.GetAllInfo();
                List<DeveloperDTO> developerDTO = new List<DeveloperDTO>();
                foreach (Developer d in fulldata)
                {
                    developerDTO.Add(d);
                }
                return Ok(developerDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }
    }
}
