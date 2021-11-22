using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Test
{
    class Tests
    {
        CleanerLogic cleanerLogic;
        TeacherLogic teacherLogic;
        StudentLogic studentLogic;
        CourseLogic courseLogic;
        public  Tests()
        {
            Teacher fakeTeacher = new Teacher()
            {
                LastName = "Benedek",
                Firstname = "Laszlo",
                Age = 45,
                Salary = 12000

            };
            var mockCleanerRepo = new  Mock<ICleanerRepository>();
            var mockTeacherRepo = new Mock<ITeacherRepository>();
            var mockCourseRepo = new Mock<ICleanerRepository>();
            var mockStudentRepo = new Mock<IStudentRepository>();
        }
        private IQueryable<Teacher> FakeTeacherRepo()
        {
            Student student1 = new() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            Student student5 = new() { StudentID = 5, Firstname = "Lakatos", LastName = "Brendon" };
            Student student6 = new() { StudentID = 6, Firstname = "Sztremi", LastName = "Akos" };
            Student student7 = new() { StudentID = 7, Firstname = "Dwayne", LastName = "Johnson" };
            //--------------------------------------------
            Teacher teacher1 = new() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500, Age = 30 };
            Teacher teacher2 = new() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333, Age = 45 };
            Teacher teacher3 = new() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945, Age = 40 };
            Teacher teacher4 = new() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200, Age = 33 };
            Teacher teacher5 = new() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000, Age = 75 };
            //--------------------------------------------
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "natural science " };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = new TimeSpan(0, 45, 0), Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, Name = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, Name = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, Name = "Francisco", Salary = null, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, Name = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, Name = "Juan", Salary = 2000, Position = "HeadCleaner" };

            student1.Courses = new List<Course>();
            student2.Courses = new List<Course>();
            student3.Courses = new List<Course>();
            student4.Courses = new List<Course>();
            student5.Courses = new List<Course>();
            student6.Courses = new List<Course>();
            student7.Courses = new List<Course>();

            teacher1.Courses = new List<Course>();
            teacher2.Courses = new List<Course>();
            teacher3.Courses = new List<Course>();
            teacher4.Courses = new List<Course>();
            teacher5.Courses = new List<Course>();

            course1.StudentId = student1.StudentID; student1.Courses.Add(course1);
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.CleanerId = cleaner1.CleanerId;

            course2.StudentId = student1.StudentID; student1.Courses.Add(course2);
            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.CleanerId = cleaner2.CleanerId;

            course3.StudentId = student3.StudentID; student3.Courses.Add(course3);
            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.StudentId = student5.StudentID; student5.Courses.Add(course3);
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course3.CleanerId = cleaner3.CleanerId;

            course4.StudentId = student7.StudentID; student7.Courses.Add(course4);
            course4.StudentId = student6.StudentID; student6.Courses.Add(course4);
            course4.StudentId = student5.StudentID; student5.Courses.Add(course4);
            course4.StudentId = student1.StudentID; student1.Courses.Add(course4);
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course4.CleanerId = cleaner4.CleanerId;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.StudentId = student2.StudentID; student2.Courses.Add(course5);
            course5.StudentId = student3.StudentID; student3.Courses.Add(course5);
            course5.StudentId = student7.StudentID; student7.Courses.Add(course5);
            course5.StudentId = student6.StudentID; student6.Courses.Add(course5);
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course5.CleanerId = cleaner5.CleanerId;

            List<Teacher> items = new List<Teacher>();
            items.Add(teacher1);
            items.Add(teacher2);
            items.Add(teacher3);
            items.Add(teacher4);
            items.Add(teacher5);

            return items.AsQueryable();
        }
        private IQueryable<Course> FakeCourseRepo()
        {
            Student student1 = new() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            Student student5 = new() { StudentID = 5, Firstname = "Lakatos", LastName = "Brendon" };
            Student student6 = new() { StudentID = 6, Firstname = "Sztremi", LastName = "Akos" };
            Student student7 = new() { StudentID = 7, Firstname = "Dwayne", LastName = "Johnson" };
            //--------------------------------------------
            Teacher teacher1 = new() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500, Age = 30 };
            Teacher teacher2 = new() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333, Age = 45 };
            Teacher teacher3 = new() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945, Age = 40 };
            Teacher teacher4 = new() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200, Age = 33 };
            Teacher teacher5 = new() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000, Age = 75 };
            //--------------------------------------------
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "natural science " };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = new TimeSpan(0, 45, 0), Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, Name = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, Name = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, Name = "Francisco", Salary = null, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, Name = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, Name = "Juan", Salary = 2000, Position = "HeadCleaner" };

            student1.Courses = new List<Course>();
            student2.Courses = new List<Course>();
            student3.Courses = new List<Course>();
            student4.Courses = new List<Course>();
            student5.Courses = new List<Course>();
            student6.Courses = new List<Course>();
            student7.Courses = new List<Course>();

            teacher1.Courses = new List<Course>();
            teacher2.Courses = new List<Course>();
            teacher3.Courses = new List<Course>();
            teacher4.Courses = new List<Course>();
            teacher5.Courses = new List<Course>();

            course1.StudentId = student1.StudentID; student1.Courses.Add(course1);
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.CleanerId = cleaner1.CleanerId;

            course2.StudentId = student1.StudentID; student1.Courses.Add(course2);
            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.CleanerId = cleaner2.CleanerId;

            course3.StudentId = student3.StudentID; student3.Courses.Add(course3);
            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.StudentId = student5.StudentID; student5.Courses.Add(course3);
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course3.CleanerId = cleaner3.CleanerId;

            course4.StudentId = student7.StudentID; student7.Courses.Add(course4);
            course4.StudentId = student6.StudentID; student6.Courses.Add(course4);
            course4.StudentId = student5.StudentID; student5.Courses.Add(course4);
            course4.StudentId = student1.StudentID; student1.Courses.Add(course4);
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course4.CleanerId = cleaner4.CleanerId;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.StudentId = student2.StudentID; student2.Courses.Add(course5);
            course5.StudentId = student3.StudentID; student3.Courses.Add(course5);
            course5.StudentId = student7.StudentID; student7.Courses.Add(course5);
            course5.StudentId = student6.StudentID; student6.Courses.Add(course5);
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course5.CleanerId = cleaner5.CleanerId;

            List<Course> items = new List<Course>();
            items.Add(course1);
            items.Add(course2);
            items.Add(course3);
            items.Add(course4);
            items.Add(course5);

            return items.AsQueryable();
        }
        private IQueryable<Student> FakeStudentRepo()
        {
            Student student1 = new() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            Student student5 = new() { StudentID = 5, Firstname = "Lakatos", LastName = "Brendon" };
            Student student6 = new() { StudentID = 6, Firstname = "Sztremi", LastName = "Akos" };
            Student student7 = new() { StudentID = 7, Firstname = "Dwayne", LastName = "Johnson" };
            //--------------------------------------------
            Teacher teacher1 = new() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500, Age = 30 };
            Teacher teacher2 = new() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333, Age = 45 };
            Teacher teacher3 = new() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945, Age = 40 };
            Teacher teacher4 = new() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200, Age = 33 };
            Teacher teacher5 = new() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000, Age = 75 };
            //--------------------------------------------
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "natural science " };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = new TimeSpan(0, 45, 0), Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, Name = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, Name = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, Name = "Francisco", Salary = null, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, Name = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, Name = "Juan", Salary = 2000, Position = "HeadCleaner" };

            student1.Courses = new List<Course>();
            student2.Courses = new List<Course>();
            student3.Courses = new List<Course>();
            student4.Courses = new List<Course>();
            student5.Courses = new List<Course>();
            student6.Courses = new List<Course>();
            student7.Courses = new List<Course>();

            teacher1.Courses = new List<Course>();
            teacher2.Courses = new List<Course>();
            teacher3.Courses = new List<Course>();
            teacher4.Courses = new List<Course>();
            teacher5.Courses = new List<Course>();

            course1.StudentId = student1.StudentID; student1.Courses.Add(course1);
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.CleanerId = cleaner1.CleanerId;

            course2.StudentId = student1.StudentID; student1.Courses.Add(course2);
            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.CleanerId = cleaner2.CleanerId;

            course3.StudentId = student3.StudentID; student3.Courses.Add(course3);
            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.StudentId = student5.StudentID; student5.Courses.Add(course3);
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course3.CleanerId = cleaner3.CleanerId;

            course4.StudentId = student7.StudentID; student7.Courses.Add(course4);
            course4.StudentId = student6.StudentID; student6.Courses.Add(course4);
            course4.StudentId = student5.StudentID; student5.Courses.Add(course4);
            course4.StudentId = student1.StudentID; student1.Courses.Add(course4);
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course4.CleanerId = cleaner4.CleanerId;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.StudentId = student2.StudentID; student2.Courses.Add(course5);
            course5.StudentId = student3.StudentID; student3.Courses.Add(course5);
            course5.StudentId = student7.StudentID; student7.Courses.Add(course5);
            course5.StudentId = student6.StudentID; student6.Courses.Add(course5);
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course5.CleanerId = cleaner5.CleanerId;

            List<Student> items = new List<Student>();
            items.Add(student1);
            items.Add(student2);
            items.Add(student3);
            items.Add(student4);
            items.Add(student5);
            items.Add(student6);
            items.Add(student7);

            return items.AsQueryable();
        }
        private IQueryable<Cleaner> FakeCleanerRepo()
        {
            Student student1 = new() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            Student student5 = new() { StudentID = 5, Firstname = "Lakatos", LastName = "Brendon" };
            Student student6 = new() { StudentID = 6, Firstname = "Sztremi", LastName = "Akos" };
            Student student7 = new() { StudentID = 7, Firstname = "Dwayne", LastName = "Johnson" };
            //--------------------------------------------
            Teacher teacher1 = new() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500, Age = 30 };
            Teacher teacher2 = new() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333, Age = 45 };
            Teacher teacher3 = new() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945, Age = 40 };
            Teacher teacher4 = new() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200, Age = 33 };
            Teacher teacher5 = new() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000, Age = 75 };
            //--------------------------------------------
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "natural science " };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = new TimeSpan(3, 30, 0), Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = new TimeSpan(0, 45, 0), Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = new TimeSpan(2, 0, 0), Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, Name = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, Name = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, Name = "Francisco", Salary = null, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, Name = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, Name = "Juan", Salary = 2000, Position = "HeadCleaner" };

            student1.Courses = new List<Course>();
            student2.Courses = new List<Course>();
            student3.Courses = new List<Course>();
            student4.Courses = new List<Course>();
            student5.Courses = new List<Course>();
            student6.Courses = new List<Course>();
            student7.Courses = new List<Course>();

            teacher1.Courses = new List<Course>();
            teacher2.Courses = new List<Course>();
            teacher3.Courses = new List<Course>();
            teacher4.Courses = new List<Course>();
            teacher5.Courses = new List<Course>();

            course1.StudentId = student1.StudentID; student1.Courses.Add(course1);
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.CleanerId = cleaner1.CleanerId;

            course2.StudentId = student1.StudentID; student1.Courses.Add(course2);
            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.CleanerId = cleaner2.CleanerId;

            course3.StudentId = student3.StudentID; student3.Courses.Add(course3);
            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.StudentId = student5.StudentID; student5.Courses.Add(course3);
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course3.CleanerId = cleaner3.CleanerId;

            course4.StudentId = student7.StudentID; student7.Courses.Add(course4);
            course4.StudentId = student6.StudentID; student6.Courses.Add(course4);
            course4.StudentId = student5.StudentID; student5.Courses.Add(course4);
            course4.StudentId = student1.StudentID; student1.Courses.Add(course4);
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course4.CleanerId = cleaner4.CleanerId;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.StudentId = student2.StudentID; student2.Courses.Add(course5);
            course5.StudentId = student3.StudentID; student3.Courses.Add(course5);
            course5.StudentId = student7.StudentID; student7.Courses.Add(course5);
            course5.StudentId = student6.StudentID; student6.Courses.Add(course5);
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course5.CleanerId = cleaner5.CleanerId;

            List<Cleaner> items = new List<Cleaner>();
            items.Add(cleaner1);
            items.Add(cleaner2);
            items.Add(cleaner3);
            items.Add(cleaner4);
            items.Add(cleaner5);

            return items.AsQueryable();
        }
    }
}
