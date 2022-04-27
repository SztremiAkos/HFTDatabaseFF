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
    public class TeacherController : ControllerBase
    {
        ITeacherLogic tl;
        IHubContext<SignalRHub> hub;

        public TeacherController(ITeacherLogic tl, IHubContext<SignalRHub> hub)
        {
            this.tl = tl;
            this.hub = hub;
        }

        // GET: /Teacher
        [HttpGet]
        public IEnumerable<Teacher> Get()
        {
            return tl.GetAllTeachers();
        }

        // GET /Teacher/5
        [HttpGet("{id}")]
        public Teacher Get(int id)
        {
            return tl.GetOneTeacher(id);
        }

        // POST /student
        [HttpPost]
        public void Post([FromBody] Teacher teacher)
        {
            tl.AddNewTeacher(teacher);
            this.hub.Clients.All.SendAsync("TeacherCreated", teacher);
        }

        // PUT /student
        [HttpPut]
        public void Put(int id, [FromBody] Teacher teacher)
        {
            tl.UpdateTeacher(teacher);
            this.hub.Clients.All.SendAsync("TeacherUpdated", teacher);
        }

        // DELETE /Teacher/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var teacher = tl.GetOneTeacher(id);
            tl.DeleteTeacher(id);
            this.hub.Clients.All.SendAsync("TeacherDeleted", teacher);
        }
    }
}
