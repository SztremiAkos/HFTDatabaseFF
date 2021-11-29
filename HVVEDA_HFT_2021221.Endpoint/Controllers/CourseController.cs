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
    public class CourseController : ControllerBase
    {
        ICourseLogic cl;

        public CourseController(ICourseLogic cl)
        {
            this.cl = cl;
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
        }

        // PUT /course/5
        [HttpPut]
        public void Put([FromBody] Course value)
        {
            cl.UpdateCourse(value);
        }

        // DELETE /course/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            cl.DeleteCourse(id);
        }
    }
}
