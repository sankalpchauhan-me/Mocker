using DBLib.Models;
using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Repository;
using Mocker.Utils;
using System;
using System.Data.SqlClient;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.REGISTER_ROUTE_PREFIX)]
    public class UserController : ApiController
    {
        private MockerRepository _repository;
        public UserController()
        {
            _repository = new MockerRepository();
        }

        [Route(Constants.REGISTER_ROUTE)]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] Developer developer)
        {
            try
            {
                Developer insertedDev= _repository.InsertDev(developer);
                if(insertedDev!=null)
                    _repository.Save();
                DeveloperDTO dto = insertedDev;
                return Created(new Uri(Url.Link(Constants.GET_DEVELOPER_BY_ID, new { id = developer.UserId })), dto);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [NotFoundActionFilter]
        [Route("{id}", Name = Constants.GET_DEVELOPER_BY_ID)]
        [HttpGet]
        public IHttpActionResult GetRegisteredUser([FromUri] string id)
        {
            try
            {
                DeveloperDTO developerViewModel = new DeveloperDTO();
                Developer d = _repository.GetDeveloperById(id);
                developerViewModel = d;
                return Ok(developerViewModel);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }
    }
}
