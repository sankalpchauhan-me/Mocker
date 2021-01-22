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
    [RoutePrefix(Constants.APP_ENTITY_ROUTE_PREFIX)]
    public class EntityController : ApiController
    {
        private MockerRepository _repository;
        public EntityController()
        {
            _repository = new MockerRepository();
        }

        //Create
        [HttpPost]
        [Route("{userid}/entity")]
        public IHttpActionResult InsertAppEntity([FromUri] string userid, [FromUri] string appname, [FromBody] AppEntity appEntity)
        {
            try
            {
                AppEntityDTO appEntityDTO = new AppEntityDTO();
                AppEntity da = _repository.InsertAppEntity(userid, appname, appEntity);
                if (da != null)
                {
                    _repository.Save();
                }
                appEntityDTO = da;
                return Created(new Uri(Url.Link(Constants.GET_ENTITY_BY_NAME, new { userid, appname, entityname = appEntityDTO.EntityName })), appEntityDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        //Read
        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/entity/{entityname}", Name = Constants.GET_ENTITY_BY_NAME)]
        public IHttpActionResult GetAppEntity([FromUri] string userid, [FromUri] string entityname,[FromUri] string appname)
        {
            try
            {
                AppEntityDTO appEntityDTO = new AppEntityDTO();
                appEntityDTO = _repository.GetAppEntity(userid, appname, entityname);
                return Ok(appEntityDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        //Update 
        [HttpPut]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult UpdateAppEntity([FromUri] string userId, [FromUri] string entityname, [FromUri] string appname, [FromBody] AppEntity appEntity)
        {
            try
            {
                if (_repository.UpdateAppEntity(userId, appname, entityname, appEntity))
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

        //Delete
        [HttpDelete]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult DeleteAppEntity([FromUri] string userid, [FromUri] string entityname, [FromUri] string appname)
        {
            try
            {
                AppEntity appEntity = _repository.DeleteAppEntity(userid, appname, entityname);
                if(appEntity != null)
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

        //Activate
        [HttpPatch]
        [Route("{userid}/entity/{entityname}")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string entityname, [FromUri] string appname, [FromUri] bool deactivation)
        {
            try
            {
                if(_repository.SetAppEntityActive(userId, appname, entityname, deactivation))
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
