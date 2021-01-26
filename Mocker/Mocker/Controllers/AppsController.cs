using DBLib.Models;
using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Service;
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
        private DevAppService _devAppService;
        public AppsController()
        {
            _devAppService = new DevAppService();
        }

        [HttpPost]
        [Route("{userid}/app")]
        public IHttpActionResult InsertApp([FromUri] string userId, [FromBody] DevApp devApp)
        {
            DevAppDTO devAppDTO = new DevAppDTO();
            devAppDTO = _devAppService.InsertDevApp(userId, devApp);
            return Created(new Uri(Url.Link(Constants.GET_APP_BY_NAME, new { userId, name = devAppDTO.AppName })), devAppDTO);

        }

        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/app", Name = Constants.GET_APP_BY_NAME)]
        public IHttpActionResult GetApp([FromUri] string userId, [FromUri] string name)
        {
            DevAppDTO dto = _devAppService.GetDevAppById(userId, name);
            if (dto != null)
            {
                return Ok(dto);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPut]
        [Route("{userid}/app")]
        public IHttpActionResult UpdateApp([FromUri] string userId, [FromUri] string name, [FromBody] DevApp devApp)
        {
            if (_devAppService.UpdateDevApp(userId, name, devApp))
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        [Route("{userid}/app")]
        public IHttpActionResult DeleteApp([FromUri] string userId, [FromUri] string name)
        {
            if (_devAppService.DeleteDevApp(userId, name) != null)
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return NotFound();
            }
        }

        // Deactivate App
        [HttpPatch]
        [Route("{userid}/app")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string name, [FromUri] bool deactivation)
        {
            if (_devAppService.SetDevAppActive(userId, name, deactivation))
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
