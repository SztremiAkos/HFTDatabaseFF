using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HVVEDA_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class statController : ControllerBase
    {
        IStudentLogic sl;
        ICourseLogic cl;

        public statController(IStudentLogic sl, ICourseLogic cl)
        {
            this.sl = sl;
            this.cl = cl;
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> StudentCountPerCategory()  //DONE
        {
            return sl.StudentCountPerCategory();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int?>> CleanerNumberPerClassroom() //course //DONE
        {
            return cl.CleanerNumberPerClassroom();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<string, int>> CourseCleaningPrice() //course //TODO
        {
            return cl.CourseCleaningPrice();
        }
        [HttpGet]
        public IEnumerable<KeyValuePair<Teacher, double?>> TeacherSalaryPerCourse() //course //TODO
        {
            return cl.TeacherSalaryPerCourse();
        }


        [HttpGet]
        public Teacher TheDirtiestCoursesTeacher() //course //DONE
        {
            return cl.GetTheDirtiestCoursesTeacher().FirstOrDefault();
        }
    }
}
