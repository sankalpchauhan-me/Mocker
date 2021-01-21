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
    [RoutePrefix(Constants.APP_ROUTE_PREFIX)]
    public class AppsController : ApiController
    {
        private MockerRepository _repository;
        public AppsController()
        {
            _repository = new MockerRepository();
        }

        [HttpPost]
        [Route("{userid}/app")]
        public IHttpActionResult InsertApp([FromUri] string userId, [FromBody] DevApp devApp)
        {
            try
            {
                DevAppDTO devAppDTO = new DevAppDTO();
                DevApp da = _repository.InsertDevApp(userId, devApp);
                if (da != null)
                {
                    _repository.Save();
                }
                devAppDTO = da;
                return Created(new Uri(Url.Link(Constants.GET_APP_BY_NAME, new { userId, name = devAppDTO.AppName })), devAppDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/app", Name = Constants.GET_APP_BY_NAME)]
        public IHttpActionResult GetApp([FromUri] string userId, [FromUri] string name)
        {
            try
            {
                DevAppDTO devAppDTO = new DevAppDTO();
                devAppDTO = _repository.GetDevApp(userId, name);
                return Ok(devAppDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("{userid}/app")]
        public IHttpActionResult UpdateApp([FromUri] string userId, [FromUri] string name, [FromBody] DevApp devApp)
        {
            try
            {
                if (_repository.UpdateDevApp(userId, name, devApp))
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
        [Route("{userid}/app")]
        public IHttpActionResult DeleteApp([FromUri] string userId, [FromUri] string name)
        {
            try
            {
                DevApp devApp = _repository.DeleteDevApp(userId, name);
                if (devApp != null)
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

        // Deactivate App
        [HttpPatch]
        [Route("{userid}/app")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string name, [FromUri] bool deactivation)
        {
            try
            {
                if (_repository.SetDevAppActive(userId, name, deactivation))
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
    }
}
