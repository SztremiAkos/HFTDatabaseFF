using HVVEDA_HFT_2021221.Logic;
using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;



namespace HVVEDA_HFT_2021221.Test
{
    class Tests
    {
        CleanerLogic cleanerLogic { get; set; }
        TeacherLogic teacherLogic { get; set; }
        StudentLogic studentLogic { get; set; }
        CourseLogic courseLogic { get; set; }


        [SetUp]
        public void SetUp()
        {
            var mockCleanerRepo = new Mock<ICleanerRepository>();
            var mockTeacherRepo = new Mock<ITeacherRepository>();
            var mockCourseRepo = new Mock<ICourseRepository>();
            var mockStudentRepo = new Mock<IStudentRepository>();

            mockCourseRepo.Setup(x => x.GetOne(It.IsAny<int>())).Returns(
                new Course()
                {
                    CourseID = 3,
                    Title = "Advanced development techniques",
                    Credits = 7,
                    Location = "F03",
                    Length = "03:30",
                    Type = "Programming"
                });
            mockTeacherRepo.Setup(x => x.ReadAll()).Returns(this.FakeTeacherRepo);
            mockCleanerRepo.Setup(x => x.ReadAll()).Returns(this.FakeCleanerRepo);
            mockCourseRepo.Setup(x => x.ReadAll()).Returns(this.FakeCourseRepo);
            mockStudentRepo.Setup(x => x.ReadAll()).Returns(this.FakeStudentRepo);

            this.studentLogic = new StudentLogic(mockStudentRepo.Object, mockCourseRepo.Object);
            this.teacherLogic = new TeacherLogic(mockTeacherRepo.Object);
            this.cleanerLogic = new CleanerLogic(mockCleanerRepo.Object);
            this.courseLogic = new CourseLogic(mockCourseRepo.Object, mockTeacherRepo.Object, mockCleanerRepo.Object, mockStudentRepo.Object);
        }
        [Test]
        public void GetOneCourse()
        {
            var CourseItem = this.courseLogic.GetOne(3);

            Assert.That(CourseItem.Title, Is.EqualTo("Advanced development techniques"));
        }

        [TestCase("", false)]
        [TestCase("History", true)]
        public void CourseCreateThrowsException(string value, bool result)
        {
            if (result)
            {
                Assert.That(() => courseLogic.AddNewCourse(new Course()
                {
                    Title = value
                }), Throws.Nothing);
            }
            else
            {
                Assert.That(() => courseLogic.AddNewCourse(new Course()
                {
                    Title = value
                }), Throws.TypeOf<NullReferenceException>());
            }

        }

        [Test]
        public void StudentCreateThrowsException()
        {
            Assert.That(() => studentLogic.AddNewStudent(new Student()
            {
                Firstname = ""
            }), Throws.TypeOf<NullReferenceException>());

        }

        [TestCase(0, 2)]
        [TestCase(1, 2)]
        [TestCase(2, 1)]
        public void StudentCountPerClassRoom_GivesTheRightAmount(int idx, int amount)
        {
            Assert.That(studentLogic.StudentCountPerCategory().OrderByDescending(x => x.Value).ToArray()[idx].Value, Is.EqualTo(amount));
        }

        [TestCase(0, 15000d)]
        [TestCase(1, 945)]
        [TestCase(2, 500)]
        [TestCase(3, 333)]
        [TestCase(4, 200)]
        public void TeacherSalaryPerCourse_ReturnsTheCorrectAmountInOrder(int idx, double? value)
        {
            var salary = courseLogic.TeacherSalaryPerCourse().OrderByDescending(x => x.Value).ToArray();
            ;
            Assert.That(salary[idx].Value, Is.EqualTo(value));
        }

        [TestCase(0, 2000)]
        [TestCase(1, 300)]
        [TestCase(2, 200)]
        [TestCase(3, 100)]
        public void CourseCleaningPrice(int idx, int? price)
        {
            var cleaningPrices = courseLogic.CourseCleaningPrice().OrderByDescending(x => x.Value).ToArray();
            Assert.That(cleaningPrices[idx].Value, Is.EqualTo(price));
        }


        [TestCase(-1)]
        [TestCase(-10)]
        [TestCase(101)]
        public void TeacherCreateThrowsException(int age)
        {
            Assert.That(() => teacherLogic.AddNewTeacher(new Teacher() { Firstname = "Laci", Age = age }), Throws.TypeOf<IndexOutOfRangeException>());
        }

        [Test]
        public void GetTheDirtiestCoursesTeacher_IsCorrect()
        {
            var dirtyTeacher = courseLogic.GetTheDirtiestCoursesTeacher().FirstOrDefault();
            Assert.That(dirtyTeacher.TeacherId, Is.EqualTo(3));
        }
        [Test]
        public void CleanerNumberPerCateg()
        {
            var Cleanernumber = courseLogic.CleanerNumberPerClassroom().ToArray();
            Assert.That(Cleanernumber[0].Value, Is.EqualTo(2));
        }
        [Test]
        public void CleanerReadAll()
        {
            Assert.That(cleanerLogic.ReadAll().Count(), Is.EqualTo(5));
        }
        private IQueryable<Teacher> FakeTeacherRepo()
        {
            Student student1 = new() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            //--------------------------------------------
            Teacher teacher1 = new() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500, Age = 30 };
            Teacher teacher2 = new() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333, Age = 45 };
            Teacher teacher3 = new() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945, Age = 40 };
            Teacher teacher4 = new() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200, Age = 33 };
            Teacher teacher5 = new() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000, Age = 75 };
            //--------------------------------------------
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = "02:00", Type = "natural science" };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = "03:00", Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = "03:30", Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = "00:45", Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = "02:00", Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, FirstName = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, FirstName = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, FirstName = "Francisco", Salary = 0, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, FirstName = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, FirstName = "Juan", Salary = 2000, Position = "HeadCleaner" };

            student1.Courses = new List<Course>();
            student2.Courses = new List<Course>();
            student3.Courses = new List<Course>();
            student4.Courses = new List<Course>();

            teacher1.Courses = new List<Course>();
            teacher2.Courses = new List<Course>();
            teacher3.Courses = new List<Course>();
            teacher4.Courses = new List<Course>();
            teacher5.Courses = new List<Course>();

            course1.StudentId = student1.StudentID; student1.Courses.Add(course1);
            course1.Student = student1;
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.Teacher = teacher1;
            course1.CleanerId = cleaner1.CleanerId;
            course1.Cleaner = cleaner1;

            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.Student = student2;
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.Teacher = teacher2;
            course2.CleanerId = cleaner2.CleanerId;
            course2.Cleaner = cleaner2;

            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.Student = student3;
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course2.Teacher = teacher2;
            course3.CleanerId = cleaner3.CleanerId;
            course3.Cleaner = cleaner3;

            course4.StudentId = student3.StudentID; student3.Courses.Add(course4);
            course4.Student = student4;
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course2.Teacher = teacher2;
            course4.CleanerId = cleaner4.CleanerId;
            course4.Cleaner = cleaner4;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.Student = student1;
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course2.Teacher = teacher2;
            course5.CleanerId = cleaner5.CleanerId;
            course5.Cleaner = cleaner5;

            cleaner1.Location = course1;
            cleaner2.Location = course2;
            cleaner3.Location = course3;
            cleaner4.Location = course4;
            cleaner5.Location = course5;


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
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = "02:00", Type = "natural science" };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = "03:00", Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = "03:30", Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = "00:45", Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = "02:00", Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, FirstName = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, FirstName = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, FirstName = "Francisco", Salary = 0, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, FirstName = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, FirstName = "Juan", Salary = 2000, Position = "HeadCleaner" };

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
            course1.Student = student1;
            course1.TeacherId = teacher1.TeacherId; teacher1.Courses.Add(course1);
            course1.Teacher = teacher1;
            course1.CleanerId = cleaner1.CleanerId;
            course1.Cleaner = cleaner1;

            course2.StudentId = student2.StudentID; student2.Courses.Add(course2);
            course2.Student = student2;
            course2.TeacherId = teacher2.TeacherId; teacher2.Courses.Add(course2);
            course2.Teacher = teacher2;
            course2.CleanerId = cleaner2.CleanerId;
            course2.Cleaner = cleaner2;

            course3.StudentId = student4.StudentID; student4.Courses.Add(course3);
            course3.Student = student3;
            course3.TeacherId = teacher3.TeacherId; teacher3.Courses.Add(course3);
            course3.Teacher = teacher3;
            course3.CleanerId = cleaner3.CleanerId;
            course3.Cleaner = cleaner3;

            course4.StudentId = student3.StudentID; student3.Courses.Add(course4);
            course4.Student = student4;
            course4.TeacherId = teacher4.TeacherId; teacher4.Courses.Add(course4);
            course4.Teacher = teacher4;
            course4.CleanerId = cleaner4.CleanerId;
            course4.Cleaner = cleaner4;

            course5.StudentId = student1.StudentID; student1.Courses.Add(course5);
            course5.Student = student1;
            course5.TeacherId = teacher5.TeacherId; teacher5.Courses.Add(course5);
            course5.Teacher = teacher5;
            course5.CleanerId = cleaner5.CleanerId;
            course5.Cleaner = cleaner5;


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
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = "02:00", Type = "natural science" };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = "03:00", Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = "03:30", Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = "00:45", Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = "02:00", Type = "McDonald's" };     
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, FirstName = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, FirstName = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, FirstName = "Francisco", Salary = 0, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, FirstName = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, FirstName = "Juan", Salary = 2000, Position = "HeadCleaner" };

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

            cleaner1.Location = course1;
            cleaner2.Location = course2;
            cleaner3.Location = course3;
            cleaner4.Location = course4;
            cleaner5.Location = course5;

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
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = "02:00", Type = "natural science" };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = "03:00", Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = "03:30", Type = "Programming" };
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = "00:45", Type = "natural science" };
            Course course5 = new() { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = "02:00", Type = "McDonald's" };            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, FirstName = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, FirstName = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, FirstName = "Francisco", Salary = 0, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, FirstName = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, FirstName = "Juan", Salary = 2000, Position = "HeadCleaner" };

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

            cleaner1.Location = course1;
            cleaner2.Location = course2;
            cleaner3.Location = course3;
            cleaner4.Location = course4;
            cleaner5.Location = course5;

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