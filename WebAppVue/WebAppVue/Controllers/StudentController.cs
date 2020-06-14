using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using WebAppVue.Models;

namespace WebAppVue.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class StudentController : ControllerBase
    {
        _2020_lenguajes_vueContext _context = new _2020_lenguajes_vueContext();

        // GET: api/Student
        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudents()
        {
            return await _context.Student.ToListAsync();
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public IEnumerable<Student> GetAllStudents()
        {
            try
            {
                return _context.Student.ToList();
            }
            catch
            {
                throw;
            }
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpGet]
        public IActionResult GetAllStudentsSP()
        {
            var students = _context.Student
                             .FromSqlRaw($"SelectStudentAPI")
                             .AsEnumerable();

            return Ok(students);
        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]/{id}")]
        [HttpGet]
        public IActionResult GetStudent(int id)
        {

            var studentId = new SqlParameter("@StudentId", id);
            var student = _context.Student
                           .FromSqlRaw($"SelectStudentByIdAPI @StudentId", studentId)
                           .AsEnumerable().Single();

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }


        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPut]
        public IActionResult PutStudent(Student student)
        {

            var result = _context.Database.ExecuteSqlRaw("InsertUpdateStudent {0}, {1}, {2}, {3}, {4}, {5}",
                                student.StudentId,
                                student.Name,
                                student.Age,
                                student.Nationality,
                                student.Major,
                                "Update");
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }


        [EnableCors("GetAllPolicy")]
        [Route("[action]")]
        [HttpPost]
        public IActionResult PostStudent(Student student)
        {

            var result = _context.Database.ExecuteSqlRaw("InsertUpdateStudent {0}, {1}, {2}, {3}, {4}, {5}",
                                student.StudentId,
                                student.Name,
                                student.Age,
                                student.Nationality,
                                student.Major,
                                "Insert");
            if (result == 0)
            {
                return null;
            }

            return Ok(result);

        }

        [EnableCors("GetAllPolicy")]
        [Route("[action]/{id}")]
        [HttpDelete]

        public IActionResult DeleteStudent(int id)
        {
            var result = _context.Database.ExecuteSqlRaw("DeleteStudent {0}", id);
            if (result == 0)
            {
                return null;
            }

            return Ok(result);
        }

        private bool StudentExists(int id)
        {
            return _context.Student.Any(e => e.StudentId == id);
        }
    }
}
