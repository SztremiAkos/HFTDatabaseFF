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
        public StudentRepository(CourseDbContext ctx): base(ctx) { } //CourseContext

        public void AddNewStudent(Student student)
        {
            ctx.Add(student);
            ctx.SaveChanges();
        } //c
        public override void DeleteOne(int id)
        {
            var stud = GetOne(id);
            
            if (stud.Courses !=null)
            {
                var courses = stud.Courses;
                foreach (var item in courses)
                {
                    if (item.StudentId == stud.StudentID)
                    {
                        item.Student = null;
                    }
                }
            }
            
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
            toUpdate.Firstname = student.Firstname;
            toUpdate.LastName = student.LastName;
            ctx.SaveChanges();
        }
    }
}
