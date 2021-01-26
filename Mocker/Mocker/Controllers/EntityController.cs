using DBLib.Models;
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
    [RoutePrefix(Constants.APP_ENTITY_ROUTE_PREFIX)]
    public class EntityController : ApiController
    {
        private AppEntityService _appEntityService;
        public EntityController()
        {
            _appEntityService = new AppEntityService();
        }

        //Create
        [HttpPost]
        [Route("{userid}/entity")]
        public IHttpActionResult InsertAppEntity([FromUri] string userid, [FromUri] string name, [FromBody] AppEntity appEntity)
        {
            AppEntityDTO appEntityDTO = new AppEntityDTO();
            AppEntityDTO da = _appEntityService.InsertAppEntity(userid, name, appEntity);
            if (da != null)
            {
                appEntityDTO = da;
                return Created(new Uri(Url.Link(Constants.GET_ENTITY_BY_NAME, new { userid, name, entityname = appEntityDTO.EntityName })), appEntityDTO);
            }
            else
            {
                return BadRequest("This entity already exists in the specified app for this user");
            }

        }

        //Read
        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/entity/{entityname}", Name = Constants.GET_ENTITY_BY_NAME)]
        public IHttpActionResult GetAppEntity([FromUri] string userid, [FromUri] string entityname, [FromUri] string name)
        {
            AppEntityDTO appEntityDTO = new AppEntityDTO();
            appEntityDTO = _appEntityService.GetAppEntity(userid, name, entityname);
            if (appEntityDTO != null)
            {
                return Ok(appEntityDTO);
            }
            else
            {
                return NotFound();
            }

        }

        //Update 
        [HttpPut]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult UpdateAppEntity([FromUri] string userId, [FromUri] string entityname, [FromUri] string name, [FromBody] AppEntity appEntity)
        {
            if (_appEntityService.UpdateAppEntity(userId, name, entityname, appEntity))
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return NotFound();
            }

        }

        //Delete
        [HttpDelete]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult DeleteAppEntity([FromUri] string userid, [FromUri] string entityname, [FromUri] string name)
        {
            AppEntityDTO appEntity = _appEntityService.DeleteAppEntity(userid, name, entityname);
            if (appEntity != null)
            {
                return StatusCode(HttpStatusCode.Accepted);
            }
            else
            {
                return NotFound();
            }
        }

        //Activate
        [HttpPatch]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string entityname, [FromUri] string name, [FromUri] bool deactivation)
        {

            if (_appEntityService.SetAppEntityActive(userId, name, entityname, deactivation))
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
