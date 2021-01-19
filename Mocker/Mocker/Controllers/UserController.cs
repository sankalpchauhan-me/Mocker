using DBLib.Models;
using Mocker.Filter;
using Mocker.Repository;
using Mocker.Utils;
using Mocker.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mocker.Controllers
{
    [ModelValidator]
    [RoutePrefix(Constants.REGISTER_ROUTE_PREFIX)]
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
            _repository.InsertDev(developer);
            _repository.Save();
            return Created(new Uri(Url.Link(Constants.GET_DEVELOPER_BY_ID, new { id = developer.UserId})), developer);
        }

        [Route("{id:alpha}", Name = Constants.GET_DEVELOPER_BY_ID)]
        [HttpGet]
        public IHttpActionResult GetRegisteredUser([FromUri]string id)
        {
            try
            {
                DeveloperViewModel developerViewModel = new DeveloperViewModel();
                Developer d = _repository.GetDeveloperById(id);
                developerViewModel = d;
                return Ok(developerViewModel);
            } catch(SqlException e)
            {
                return InternalServerError(e);
            }
        }
    }
}
