using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(DbContext ctx) : base(ctx) { }

        public void AddNewTeacher(Teacher teacher)
        {
            ctx.Add(teacher);
            ctx.SaveChanges();
        } //c
        public void ChangeSalary(int id, int newsalary)
        {
            var toChange = GetOne(id);
            toChange.Salary = newsalary;
            ctx.SaveChanges();
        } //u
        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        } //d
        public ICollection<Course> GetCourses(int id)
        {
            return GetOne(id).Courses.ToList();
        } //r
        public override Teacher GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.TeacherId==id);
        } //r

        public void UpdateTeacher(Teacher teacher)
        {
            var toUpdate = GetOne(teacher.TeacherId);
            toUpdate.Salary = teacher.Salary;
            toUpdate.Courses = teacher.Courses;
            ctx.SaveChanges();
        }
    }
}
