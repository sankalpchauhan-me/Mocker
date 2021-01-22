using DBLib.Models;
using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Repository;
using Mocker.Utils;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.USER_ROUTE_PREFIX)]
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
                Developer insertedDev = _repository.InsertDev(developer);
                if (insertedDev != null)
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
                DeveloperDTO developerDTO = new DeveloperDTO();
                Developer d = _repository.GetDeveloperById(id);
                developerDTO = d;
                return Ok(developerDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateRegisteredUser([FromUri] string id, [FromBody] Developer modifiedDeveloper)
        {
            try
            {
                DeveloperDTO developerDTO = new DeveloperDTO();
                if (_repository.UpdateDeveloper(id, modifiedDeveloper))
                {
                    _repository.Save();
                    return StatusCode(HttpStatusCode.Accepted);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public IHttpActionResult DeleteRegisteredUser([FromUri] string id)
        {
            try
            {
                DeveloperDTO developerDTO = new DeveloperDTO();
                if (_repository.DeleteDeveloperById(id) != null)
                {
                    //Hard Delete
                    _repository.Save();
                    return StatusCode(HttpStatusCode.Accepted);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        // Deactivate User
        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult SetUserActivation([FromUri] string id, [FromUri] bool deactivation)
        {
            try
            {
                if (_repository.SetDeveloperActive(id, deactivation))
                {
                    //SOFT Delete
                    _repository.Save();
                    return StatusCode(HttpStatusCode.Accepted);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

    }
}
