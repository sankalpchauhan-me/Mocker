using DBModels.Models;
using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Service;
using Mocker.Utils;
using System;
using System.Net;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.USER_ROUTE_PREFIX)]
    public class UserController : ApiController
    {
        private DeveloperService _developerService;

        public UserController()
        {
            _developerService = new DeveloperService();
        }

        [Route(Constants.REGISTER_ROUTE)]
        [HttpPost]
        public IHttpActionResult RegisterUser([FromBody] Developer developer)
        {
            DeveloperDTO dto = _developerService.InsertDev(developer);
            return Created(new Uri(Url.Link(Constants.GET_DEVELOPER_BY_ID, new { id = developer.UserId })), dto);

        }

        [NotFoundActionFilter]
        [Route("{id}", Name = Constants.GET_DEVELOPER_BY_ID)]
        [HttpGet]
        public IHttpActionResult GetRegisteredUser([FromUri] string id)
        {
            DeveloperDTO dto = _developerService.GetDeveloperById(id);
            if (dto != null)
                return Ok(dto);
            else
                return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public IHttpActionResult UpdateRegisteredUser([FromUri] string id, [FromBody] Developer modifiedDeveloper)
        {
            if (_developerService.UpdateDeveloper(id, modifiedDeveloper))
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{id}")]
        //Hard Delete
        public IHttpActionResult DeleteRegisteredUser([FromUri] string id)
        {
            if (_developerService.DeleteDeveloper(id) != null)
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();
        }

        // Deactivate User
        //SOFT Delete
        [HttpPatch]
        [Route("{id}")]
        public IHttpActionResult SetUserActivation([FromUri] string id, [FromUri] bool deactivation)
        {
            if (_developerService.SetDeveloperActive(id, deactivation))
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();
        }

    }

}
