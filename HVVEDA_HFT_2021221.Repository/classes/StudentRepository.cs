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
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        public StudentRepository(DbContext ctx): base(ctx) { }

        public void AddNewStudent(Student student)
        {
            ctx.Add(student);
            ctx.SaveChanges();
        } //c
        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        } //d
        public ICollection<Course> GetAllCourses(int id)
        {
            return GetOne(id).Courses.ToList();
        } // u
        public override Student GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.StudentID == id);
        } //r

        public void UpdateStudent(Student student)
        {
            var toUpdate = GetOne(student.StudentID);
            toUpdate.Courses = student.Courses;
            ctx.SaveChanges();
        }
    }
}
