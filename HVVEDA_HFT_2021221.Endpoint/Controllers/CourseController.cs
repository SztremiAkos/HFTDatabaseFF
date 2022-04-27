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
    public class CourseController : ControllerBase
    {
        ICourseLogic cl;
        IHubContext<SignalRHub> hub;

        public CourseController(ICourseLogic cl, IHubContext<SignalRHub> hub)
        {
            this.cl = cl;
            this.hub = hub;
        }

        // GET: /Course
        [HttpGet]
        public IEnumerable<Course> Get()
        {
            return cl.GetAllCourses();
        }

        // GET /Course/5
        [HttpGet("{id}")]
        public Course Get(int id)
        {
            return cl.GetOne(id);
        }

        // POST /course
        [HttpPost]
        public void Post([FromBody] Course value)
        {
            cl.AddNewCourse(value);
            this.hub.Clients.All.SendAsync("CourseCreated", value);
        }

        // PUT /course/5
        [HttpPut]
        public void Put([FromBody] Course value)
        {
            cl.UpdateCourse(value);
            this.hub.Clients.All.SendAsync("CourseUpdated", value);
        }

        // DELETE /course/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var course = cl.GetOne(id);
            cl.DeleteCourse(id);
            this.hub.Clients.All.SendAsync("CourseDeleted", course);
        }
    }
}
