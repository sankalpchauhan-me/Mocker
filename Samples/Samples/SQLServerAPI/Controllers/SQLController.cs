using System;
using System.Linq;
using System.Web.Http;
using SQLServerAPI.ActionResultCustom;
using SQLServerAPI.Filter;
using SQLServerAPI.Repository;
using SQLService;


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
        public IHttpActionResult Get(string gender = "All")
        {
                switch (gender.ToLower())
                {
                    case "all":
                        return Ok(_dbRepository.GetAll());
                    case "male":
                        return Ok(_dbRepository.GetByGender(gender));
                    case "female":
                        return Ok(_dbRepository.GetByGender(gender));
                    default:
                        return BadRequest("Query gender only accepts all, male and female and not " + gender);
                }
               
            }

        [Route("{id:int}", Name ="GetEmployeeById")]
        [HttpGet]
        public IHttpActionResult GetEmployees(int id)
        {
            var entity = _dbRepository.GetById(id);
            //return entity != null ? Ok(entity) : NotFound();
            if (entity != null)
                return Ok(entity);
            else
                return NotFound();

        }

        [Route("add")]
        [HttpPost]
        public IHttpActionResult AddEmployee([FromBody]Employee employee)
        {
            try
            {
                _dbRepository.Insert(employee);
                _dbRepository.Save();
                return Created(new Uri(Url.Link("GetEmployeeById", new { id = employee.ID })), employee);
            }
            catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }


        [Route("{id:int}")]
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            try
            {
                Employee entity = _dbRepository.Delete(id);
                if (entity != null)
                {
                    _dbRepository.Save();
                    return Ok(entity);
                }
                else
                    return NotFound();
            } catch (Exception e)
            {
                return BadRequest("" + e);
            }
        }

        [Route("update/{id:int}")]
        [HttpPut]
        public IHttpActionResult UpdateEmployee(int id, [FromBody] Employee employee)
        {
            try
            {
                Employee entity = _dbRepository.Update(employee);
                if (entity != null)
                {
                    _dbRepository.Save();
                    return Ok(entity);
                }
                else
                    return NotFound();
            }
            catch(Exception e)
            {
                return BadRequest("" + e);
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