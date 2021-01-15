using SQLServerAPI.ActionResultCustom;
using SQLServerAPI.Filter;
using SQLServerAPI.Repository;
using SQLServerAPI.Utils;
using SQLService;
using System;
using System.Data.SqlClient;
using System.Web.Http;


namespace SQLServerAPI.Controllers
{
    [ModelValidator]
    [RoutePrefix("api/employees")]
    public class SQLController : ApiController
    {
        private IDBRepository _dbRepository;

        public SQLController()
        {
            _dbRepository = new DBRepository();
        }

        [Route("")]
        public IHttpActionResult Get(Gender gender = Gender.all)
        {
            try
            {
                switch (gender)
                {
                    case Gender.all:
                        return Ok(_dbRepository.GetAll());
                    case Gender.male:
                        return Ok(_dbRepository.GetByGender("male"));
                    case Gender.female:
                        return Ok(_dbRepository.GetByGender("female"));
                    default:
                        return BadRequest("Query gender only accepts all, male and female and not " + gender);
                }
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }

        }

        [Route("{id:int}", Name = "GetEmployeeById")]
        [HttpGet]
        public IHttpActionResult GetEmployees(int id)
        {
            try
            {
                var entity = _dbRepository.GetById(id);
                if (entity != null)
                    return Ok(entity);
                else
                    return NotFound();
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }

        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody] Employee employee)
        {
            try
            {
                _dbRepository.Insert(employee);
                _dbRepository.Save();
                return Created(new Uri(Url.Link("GetEmployeeById", new { id = employee.ID })), employee);
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                //TODO: Soft Delete
                Employee entity = _dbRepository.Delete(id);
                if (entity != null)
                {
                    _dbRepository.Save();
                    return Ok(entity);
                }
                else
                    return NotFound();
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [Route("update/{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                employee.ID = id;
                //TODO: Soft Update
                Employee entity = _dbRepository.Update(employee);
                if (entity != null)
                {
                    _dbRepository.Save();
                    return Ok(entity);
                }
                else
                    return NotFound();
            }
            catch (SqlException e)
            {
                return InternalServerError(e);
            }
        }

        [Route("magic/{name:alpha}")]
        [HttpGet]
        public CustomActionResult GetSecret(string name)
        {
            return new CustomActionResult(name);
        }
    }

}