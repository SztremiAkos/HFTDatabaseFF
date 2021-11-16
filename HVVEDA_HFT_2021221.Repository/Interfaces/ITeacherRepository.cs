using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public interface ITeacherRepository : IRepository<Teacher>
    {
        void ChangeSalary(int id, int newsalary);
        ICollection<Course> GetCourses(int id);
        void AddNewTeacher(Teacher teacher);

        public void UpdateTeacher(Teacher teacher);
    }
}
