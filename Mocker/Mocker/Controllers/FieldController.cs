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
    [RoutePrefix(Constants.APP_FIELD_ROUTE_PREFIX)]
    public class FieldController : ApiController
    {
        private MockerRepository _repository;
        public FieldController()
        {
            _repository = new MockerRepository();
        }

        [HttpPost]
        [Route("{userid}/field/{entityname}")]
        public IHttpActionResult InsertEntityField([FromUri] string userId, [FromUri] string entityname, [FromBody] EntityField entityField, [FromUri] string appname)
        {
            try
            {
                EntityFieldDTO devAppDTO = new EntityFieldDTO();
                EntityField da = _repository.InsertEntityField(userId, appname, entityname, entityField);
                if (da != null)
                {
                    _repository.Save();
                }
                else
                {
                    return BadRequest("This field already exists in the specified entity for this user");
                }
                devAppDTO = da;
                return Created(new Uri(Url.Link(Constants.GET_FIELD_BY_NAME, new { userId, entityname, entityField.FieldName, appname })), devAppDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/field/{entityname}/{fieldName}", Name = Constants.GET_FIELD_BY_NAME)]
        public IHttpActionResult GetApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldName, [FromUri] string appname)
        {
            try
            {
                EntityFieldDTO entityFieldDTO = new EntityFieldDTO();
                entityFieldDTO = _repository.GetEntityField(userId, appname, entityname, fieldName);
                return Ok(entityFieldDTO);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [HttpPut]
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult UpdateApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldName, [FromBody] EntityField entityField, [FromUri] string appName)
        {
            try
            {
                if (_repository.UpdateEntityField(userId, appName, entityname, fieldName, entityField))
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
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult DeleteApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldname, [FromUri] string appName)
        {
            try
            {
                EntityField devApp = _repository.DeleteEntityField(userId, appName, entityname, fieldname);
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

        // Deactivate Field
        [HttpPatch]
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldname, [FromUri] string appName, [FromUri] bool deactivation)
        {
            try
            {
                if (_repository.SetEntityFieldActive(userId, appName, entityname, fieldname, deactivation))
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
