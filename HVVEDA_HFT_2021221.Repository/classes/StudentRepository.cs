using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    class StudentRepository : IStudentRepository
    {
        public Student DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public ICollection<Course> GetAllCourses(int id)
        {
            throw new NotImplementedException();
        }

        public Student GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Student> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
