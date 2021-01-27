using DBLib.Models;
using Mocker.DTOs;
using Mocker.Filter;
using Mocker.Service;
using Mocker.Utils;
using System.Collections.Generic;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.MAIN_ROUTE_PREFIX)]
    public class MainController : ApiController
    {

        private MainService _service;

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
    }
}
