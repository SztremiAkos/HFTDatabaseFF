using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext ctx): base(ctx) { }
        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public ICollection<Course> GetAllCourses(int id)
        {
            return GetOne(id).Courses.ToList();
        }


        public override Student GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.StudentID == id);
        }
    }
}
