using HVVEDA_HFT_2021221.Data;
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
            var teach = GetOne(id);
            var courses = teach.Courses;
            if (courses != null)
            {
                foreach (var item in courses)
                {
                    if (item.TeacherId == teach.TeacherId)
                    {
                        item.Teacher = null;
                    }
                }
            }
            ctx.SaveChanges();
        } //d
        public ICollection<Course> GetCourses(int id)
        {
            return GetOne(id).Courses.ToList();
        } //r
        public override Teacher GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.TeacherId == id);
        } //r

        public void UpdateTeacher(Teacher teacher)
        {
            var toUpdate = GetOne(teacher.TeacherId);
            toUpdate.Firstname = teacher.Firstname;
            toUpdate.LastName = teacher.LastName;
            toUpdate.Age = teacher.Age;
            toUpdate.Salary = teacher.Salary;
            toUpdate.Courses = teacher.Courses;
            ctx.SaveChanges();
        }
    }
}
