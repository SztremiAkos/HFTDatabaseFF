using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HVVEDA_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        IStudentLogic sl;
        public StudentController(IStudentLogic sl)
        {
            this.sl = sl;
        }
        // GET: /Student
        [HttpGet]
        public IEnumerable<Student> Get()
        {
            return sl.GetAllStudents();
        }

        // GET Student/5
        [HttpGet("{id}")]
        public Student Get(int id)
        {
            return sl.GetStudentbyId(id);
        }

        // POST /Student
        [HttpPost]
        public void Post([FromBody] Student student)
        {
            sl.AddNewStudent(student);
        }

        // PUT api/Student
        [HttpPut("{id}")]
        public void Put([FromBody] Student student)
        {
            sl.UpdateStudent(student);
        }
          
        // DELETE /student/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            sl.DelStudentbyId(id);
        }
    }
}
