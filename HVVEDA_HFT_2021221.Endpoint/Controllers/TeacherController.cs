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
    public class TeacherController : ControllerBase
    {
        ITeacherLogic tl;

        public TeacherController(ITeacherLogic tl)
        {
            this.tl = tl;
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
        }

        // PUT /student
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Teacher teacher)
        {
            tl.UpdateTeacher(teacher);
        }

        // DELETE /Teacher/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            tl.DeleteTeacher(id);
        }
    }
}
