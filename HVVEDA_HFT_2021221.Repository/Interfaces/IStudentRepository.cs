using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        ICollection<Course> GetAllCourses(int id);
        void AddNewStudent(Student student);

        public void UpdateStudent(Student student);
    }
}
