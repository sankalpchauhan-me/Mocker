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
    [RoutePrefix(Constants.APP_FIELD_ROUTE_PREFIX)]
    public class FieldController : ApiController
    {
        private EntityFieldService _entityFieldService;
        public FieldController()
        {
            _entityFieldService = new EntityFieldService();
        }

        [HttpPost]
        [Route("{userid}/field/{entityname}")]
        public IHttpActionResult InsertEntityField([FromUri] string userId, [FromUri] string entityname, [FromBody] EntityField entityField, [FromUri] string name)
        {
            EntityFieldDTO devAppDTO = _entityFieldService.InsertEntityField(userId, name, entityname, entityField);
            if (devAppDTO != null)
                return Created(new Uri(Url.Link(Constants.GET_FIELD_BY_NAME, new { userId, entityname, entityField.FieldName, name })), devAppDTO);
            else
                return BadRequest("This field already exists in the specified entity for this user");
        }

        [HttpGet]
        [NotFoundActionFilter]
        [Route("{userid}/field/{entityname}/{fieldName}", Name = Constants.GET_FIELD_BY_NAME)]
        public IHttpActionResult GetApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldName, [FromUri] string name)
        {
            EntityFieldDTO entityFieldDTO = new EntityFieldDTO();
            entityFieldDTO = _entityFieldService.GetEntityField(userId, name, entityname, fieldName);
            if (entityFieldDTO != null)
                return Ok(entityFieldDTO);
            else
                return NotFound();
        }

        [HttpPut]
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult UpdateApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldName, [FromBody] EntityField entityField, [FromUri] string name)
        {
            if (_entityFieldService.UpdateEntityField(userId, name, entityname, fieldName, entityField))
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();
        }

        [HttpDelete]
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult DeleteApp([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldname, [FromUri] string name)
        {

            EntityFieldDTO devApp = _entityFieldService.DeleteEntityField(userId, name, entityname, fieldname);
            if (devApp != null)
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();

        }

        // Deactivate Field
        [HttpPatch]
        [Route("{userid}/field/{entityname}/{fieldname}")]
        public IHttpActionResult SetUserActivation([FromUri] string userId, [FromUri] string entityname, [FromUri] string fieldname, [FromUri] string name, [FromUri] bool deactivation)
        {
            if (_entityFieldService.SetEntityFieldActive(userId, name, entityname, fieldname, deactivation))
                return StatusCode(HttpStatusCode.Accepted);
            else
                return NotFound();
        }
    }
}
