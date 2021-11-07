using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    interface IStudentRepository : IRepository<Student>
    {
        ICollection<Course> GetAllCourses(int id);
    }
}
