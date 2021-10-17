using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Data
{
    public class CourseContext : DbContext
    {
        public virtual DbSet<Course> Courses { get; set; }
        public virtual DbSet<Teacher> Teachers { get; set; }
        public virtual DbSet<Student> Students { get; set; }
        public CourseContext(DbContextOptions<CourseContext> options) : base(options) { }

        public CourseContext()
        {
            this.Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder
                    .UseLazyLoadingProxies()
                    .UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //1 teacher -> 1 course
            //1 student -> n course
            //

            modelBuilder.Entity<Course>(entity =>
           {
               entity
               .HasOne(course => course.Teacher)
               .WithOne(teacher => teacher.Teaches)
               .HasForeignKey<Teacher>(course => course.TeacherId)
               .OnDelete(DeleteBehavior.ClientSetNull);

           });

            modelBuilder.Entity<Course>(entity =>
            {
                entity
                .HasOne(course => course.Student)
                .WithMany(student => student.Courses)
                .HasForeignKey(repa => repa.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            });

            Student student1 = new Student() { StudentID = 1, Firstname = "John", LastName = "Cena" };
            Student student2 = new Student() { StudentID = 2, Firstname = "Benedek", LastName = "Elek" };
            Student student3 = new Student() { StudentID = 3, Firstname = "Kiss", LastName = "Laszlo" };
            Student student4 = new Student() { StudentID = 4, Firstname = "Olah", LastName = "Kiara" };
            Student student5 = new Student() { StudentID = 5, Firstname = "Lakatos", LastName = "Brendon" };
            Student student6 = new Student() { StudentID = 6, Firstname = "Sztremi", LastName = "Akos" };
            Student student7 = new Student() { StudentID = 7, Firstname = "Dwayne", LastName = "Johnson" };
            //--------------------------------------------
            Teacher teacher1 = new Teacher() { TeacherId = 1, Firstname = "Ablakos", LastName = "Laszlo", Salary = 500 };
            Teacher teacher2 = new Teacher() { TeacherId = 2, Firstname = "Asztalos", LastName = "Sandor", Salary = 333 };
            Teacher teacher3 = new Teacher() { TeacherId = 3, Firstname = "Kovacs", LastName = "Mate", Salary = 945 };
            Teacher teacher4 = new Teacher() { TeacherId = 4, Firstname = "Jakus", LastName = "Roland", Salary = 200 };
            Teacher teacher5 = new Teacher() { TeacherId = 5, Firstname = "Labnelkuli", LastName = "Botond", Salary = 15000 };
            //--------------------------------------------
            Course course1 = new Course() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), StudentsCount = 45 };
            Course course2 = new Course() { CourseID = 1, Title = "Python Programming", Credits = 6, Location = "F01", Length = new TimeSpan(3, 30, 0), StudentsCount = 45 };
            Course course3 = new Course() { CourseID = 1, Title = "Advanced development techniques", Credits = 6, Location = "F01", Length = new TimeSpan(3, 30, 0), StudentsCount = 45 };
            Course course4 = new Course() { CourseID = 1, Title = "Physics", Credits = 6, Location = "F01", Length = new TimeSpan(0, 45, 0), StudentsCount = 45 };
            Course course5 = new Course() { CourseID = 1, Title = "Mc'Donalds basics", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0), StudentsCount = 45 };


            course1.TheTeacher = teacher1;
            teacher1.Teaches = course1;

            teacher2.Teaches = course2;
            course2.TheTeacher = teacher2;

            teacher3.Teaches = course3;
            course3.TheTeacher = teacher3;

            teacher4.Teaches = course4;
            course4.TheTeacher = teacher4;

            course1.StudentId = student1.StudentID;

            
            course2.StudentId = student2.StudentID;
            course2.StudentId = student3.StudentID;

            course3.StudentId = student1.StudentID;
            course3.StudentId = student2.StudentID;
            course3.StudentId = student3.StudentID;

            course4.StudentId = student7.StudentID;
            course4.StudentId = student6.StudentID;
            course4.StudentId = student5.StudentID;
            course4.StudentId = student1.StudentID;


        }
    }
}
