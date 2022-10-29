using Cwiczenie3.Models;
using Cwiczenie3.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Cwiczenie3.Controllers
{
    [Route("students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private ICsvDatabaseService _db;

        public StudentsController(ICsvDatabaseService db)
        {
            _db = db;
        }
        
        // GET: api/<StudentsController>
        [HttpGet]
        public IActionResult Get()
        {
            var students = _db.GetStudents();
            return Ok(students);
        }

        // GET api/<StudentsController>/5
        [HttpGet("{studentIndex}")]
        public IActionResult Get([FromRoute]string studentIndex)
        {
            return Ok(_db.GetStudent(studentIndex));
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            try
            {
                return Ok(_db.CreateStudent(student));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        // PUT api/<StudentsController>/5
        [HttpPut("{studentIndex}")]
        public IActionResult Put([FromRoute]string studentIndex, [FromBody] Student student)
        {
            return Ok(_db.UpdateStudent(studentIndex, student));
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{studentIndex}")]
        public IActionResult Delete([FromRoute] string studentIndex)
        {
            try
            {
                return Ok(_db.DeleteStudent(studentIndex));
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
