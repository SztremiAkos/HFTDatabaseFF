using HVVEDA_HFT_2021221.Endpoint.Services;
using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        IHubContext<SignalRHub> hub;
        public StudentController(IStudentLogic sl, IHubContext<SignalRHub> hub)
        {
            this.sl = sl;
            this.hub = hub;
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
            this.hub.Clients.All.SendAsync("StudentCreated", student);
        }

        // PUT api/Student
        [HttpPut]
        public void Put([FromBody] Student student)
        {
            sl.UpdateStudent(student);
            this.hub.Clients.All.SendAsync("StudentUpdated", student);
        }
          
        // DELETE /student/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var student = sl.GetStudentbyId(id);   
            sl.DelStudentbyId(id);
            this.hub.Clients.All.SendAsync("StudentDeleted", student);
        }
    }
}
