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
        public virtual DbSet<Cleaner> Cleaners { get; set; }
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
               .WithMany(teacher => teacher.Courses)
               .HasForeignKey(course => course.TeacherId)
               .OnDelete(DeleteBehavior.ClientSetNull);

               entity
                .HasOne(course => course.Student)
                .WithMany(student => student.Courses)
                .HasForeignKey(student => student.StudentId)
                .OnDelete(DeleteBehavior.ClientSetNull);
               entity
               .HasOne(course => course.Cleaner)
               .WithOne(cleaner => cleaner.Location)
               .OnDelete(DeleteBehavior.ClientSetNull);
           }
           );

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
            Course course1 = new() { CourseID = 1, Title = "Calculus", Credits = 6, Location = "F01", Length = new TimeSpan(2, 0, 0),Type = "natural science " };
            Course course2 = new() { CourseID = 2, Title = "Python Programming", Credits = 3, Location = "F02", Length = new TimeSpan(3, 30, 0),Type = "Programming" };
            Course course3 = new() { CourseID = 3, Title = "Advanced development techniques", Credits = 7, Location = "F03", Length = new TimeSpan(3, 30, 0) ,Type = "Programming"};
            Course course4 = new() { CourseID = 4, Title = "Physics", Credits = 4, Location = "2.02", Length = new TimeSpan(0, 45, 0), Type = "natural science" };
            Course course5 = new () { CourseID = 5, Title = "McDonald's basics", Credits = 1, Location = "F01", Length = new TimeSpan(2, 0, 0),Type = "McDonald's" };
            //--------------------------------------------

            Cleaner cleaner1 = new() { CleanerId = 1, Name = "Rodriguez", Salary = 200, Position = "Newbie" };
            Cleaner cleaner2 = new() { CleanerId = 2, Name = "Consuela", Salary = 100, Position = "TheLazyOne" };
            Cleaner cleaner3 = new() { CleanerId = 3, Name = "Francisco", Salary = null, Position = "Fired" };
            Cleaner cleaner4 = new() { CleanerId = 4, Name = "Antonio", Salary = 300, Position = " DishWasher" };
            Cleaner cleaner5 = new() { CleanerId = 5, Name = "Juan", Salary = 2000, Position = "HeadCleaner" };

            course1.StudentId = student1.StudentID;
            course1.TeacherId = teacher1.TeacherId;
            course1.CleanerId = cleaner1.CleanerId;

            course2.StudentId = student1.StudentID;
            course2.StudentId = student2.StudentID;
            course2.TeacherId = teacher2.TeacherId;
            course2.CleanerId = cleaner2.CleanerId;

            course3.StudentId = student3.StudentID;
            course3.StudentId = student4.StudentID;
            course3.StudentId = student5.StudentID;
            course3.TeacherId = teacher3.TeacherId;
            course3.CleanerId = cleaner3.CleanerId;

            course4.StudentId = student7.StudentID;
            course4.StudentId = student6.StudentID;
            course4.StudentId = student5.StudentID;
            course4.StudentId = student1.StudentID;
            course4.TeacherId = teacher4.TeacherId;
            course4.CleanerId = cleaner4.CleanerId;

            course5.StudentId = student1.StudentID;
            course5.StudentId = student2.StudentID;
            course5.StudentId = student3.StudentID;
            course5.StudentId = student7.StudentID;
            course5.StudentId = student6.StudentID;
            course5.TeacherId = teacher5.TeacherId;
            course5.CleanerId = cleaner5.CleanerId;

            modelBuilder.Entity<Course>().HasData(course1, course2, course3, course4, course5);
            modelBuilder.Entity<Teacher>().HasData(teacher1, teacher2, teacher3, teacher4, teacher5);
            modelBuilder.Entity<Student>().HasData(student1, student2, student3, student4, student5, student6, student7);
            modelBuilder.Entity<Cleaner>().HasData(cleaner1, cleaner2, cleaner3, cleaner4, cleaner5);


        }
    }
}
