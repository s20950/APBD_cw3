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
            Student student = _db.GetStudent(studentIndex);
            if (student == null) return NotFound("No records found");
            return Ok(student);
        }

        // POST api/<StudentsController>
        [HttpPost]
        public IActionResult Post([FromBody] Student student)
        {
            int recordsChanged = _db.CreateStudent(student);
            if (recordsChanged == 0) return BadRequest();
            return Ok($"Added {recordsChanged} record");
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{studentIndex}")]
        public IActionResult Put([FromRoute]string studentIndex, [FromBody] Student student)
        {
            int recordsChanged = _db.UpdateStudent(studentIndex,student);
            if (recordsChanged == 0) return BadRequest();
            return Ok($"Modified {recordsChanged} record");
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{studentIndex}")]
        public IActionResult Delete([FromRoute] string studentIndex)
        {
            try
            {
                int recordsChanged = _db.DeleteStudent(studentIndex);
                return Ok($"Removed {recordsChanged} record");
            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
