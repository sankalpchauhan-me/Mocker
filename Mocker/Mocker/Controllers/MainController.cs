using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Service;
using Mocker.Utils;
using MockLogic;
using System.Collections.Generic;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.MAIN_ROUTE_PREFIX)]
    public class MainController : ApiController
    {

        private MainService _service;
        private MockBuilder _lib;

        public MainController()
        {
            _service = new MainService();
            
        }

        [Route(Constants.DEFAULT_ROUTE)]
        public IHttpActionResult GetAll()
        {
            List<DeveloperDTO> developerDTOs = new List<DeveloperDTO>();
            developerDTOs = _service.GetAll();
            if (developerDTOs != null)
                return Ok(developerDTOs);
            else
                return NotFound();
        }

        [Route("{userid}/{entityname}/mock")]
        public IHttpActionResult GetMockData([FromUri]string userid, [FromUri]string entityname, [FromUri]string name)
        {
            AppEntityDTO appEntityDTOs = new AppEntityDTO();
            appEntityDTOs = _service.GetAppEntity(userid, name, entityname);
            List<string> fieldNames = new List<string>();
            List<string> fieldTypes = new List<string>();
            foreach (EntityFieldDTO ef in appEntityDTOs.EntityFields)
            {
                fieldNames.Add(ef.FieldName);
                fieldTypes.Add(ef.FieldType);
            }
            _lib = new MockBuilder(fieldNames.ToArray(), fieldTypes.ToArray(), appEntityDTOs.EntityName);
            if (appEntityDTOs != null)
                return Ok(_lib.GetMocks());
            else
                return NotFound();
        }
    }
}
