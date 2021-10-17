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

            //modelBuilder.Entity<Course>(entity =>
            //{
            //    entity
            //    .HasOne(course => course.Teacher)
            //    .WithOne(teacher => teacher.Course)
            //    .HasForeignKey(course => course)
            //    .OnDelete(DeleteBehavior.ClientSetNull);
                
            //});

        }
    }
}
