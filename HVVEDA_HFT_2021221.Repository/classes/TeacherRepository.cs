using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    class TeacherRepository : Repository<Teacher>,ITeacherRepository
    {
        public TeacherRepository(DbContext ctx) : base(ctx) { }
        public void ChangeSalary(int id, int newsalary)
        {
            var toChange = GetOne(id);
            toChange.Salary = newsalary;
            ctx.SaveChanges();
        }

        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public ICollection<Course> GetCourses(int id)
        {
            return GetOne(id).Courses.ToList();
        }

        public override Teacher GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.TeacherId==id);
        }

    }
}
