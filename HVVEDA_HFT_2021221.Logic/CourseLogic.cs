using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Logic
{
    public interface ICourseLogic
    {
        #region CRUD
        void ChangeCreditAmount(int id, int newCreditAmount);
        void AddNewCourse(Course course);
        void ChangeTitle(int id, string NewTitle);
        void ChangeLocation(int id, string NewLocation);
        public void UpdateCourse(Course course);
        void DeleteCourse(int id);
        IList<Course> GetAllCourses();
        Course GetOne(int id);
        #endregion

        #region NON-CRUD
        IEnumerable<KeyValuePair<string, int?>> CleanerNumberPerClassroom();
        IEnumerable<Teacher> GetTheDirtiestCoursesTeacher();

        IEnumerable<KeyValuePair<string, int?>> CourseCleaningPrice();
        IEnumerable<KeyValuePair<string, double?>> TheMostExpensiveCourseAndTheCost();//TeacherSalaryPerCourse();




        #endregion
    }
    public class CourseLogic : ICourseLogic
    {
        ICourseRepository courseRepo;
        ITeacherRepository teacherRepo;
        ICleanerRepository cleanerRepo;
        IStudentRepository studentRepo;



        public CourseLogic(ICourseRepository courseRepo, ITeacherRepository teacherRepo, ICleanerRepository cleanerRepo, IStudentRepository studentRepo)
        {
            this.courseRepo = courseRepo;
            this.teacherRepo = teacherRepo;
            this.cleanerRepo = cleanerRepo;
            this.studentRepo = studentRepo;
        }

        public IEnumerable<Teacher> GetTheDirtiestCoursesTeacher()
        {
            var dirtyC_sub = from x in courseRepo.ReadAll()
                             where x.Cleaner.Position == "Fired"
                             select x;
            ;


            var dirtyC_SingleSub = dirtyC_sub.FirstOrDefault();
            ;
            var dirtyClass = from x in teacherRepo.ReadAll()
                             where dirtyC_SingleSub.TeacherId.Equals(x.TeacherId)
                             select x;
            return dirtyClass;

        }

        public void AddNewCourse(Course course)
        {
            if (course.Title == "" || course.Title == null)
            {
                throw new NullReferenceException("Title cant be empty");

            }
            else
            {
                courseRepo.AddNewCourse(course);
            }

        }

        public void ChangeCreditAmount(int id, int newCreditAmount)
        {
            if (id < courseRepo.ReadAll().Count())
                courseRepo.ChangeCreditAmount(id, newCreditAmount);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public void ChangeLocation(int id, string NewLocation)
        {
            if (id < courseRepo.ReadAll().Count())
                courseRepo.ChangeLocation(id, NewLocation);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public void ChangeTitle(int id, string NewTitle)
        {
            if (id < courseRepo.ReadAll().Count())
                courseRepo.ChangeTitle(id, NewTitle);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public IEnumerable<KeyValuePair<string, int?>> CleanerNumberPerClassroom()
        {
            var cleanergrp_sub = from x in cleanerRepo.ReadAll()
                                 group x by x.Location.Location into g
                                 select new KeyValuePair<string, int?>(g.Key, g.Count());
            return cleanergrp_sub;
        }

        public void UpdateCourse(Course course)
        {
            courseRepo.UpdateCourse(course);
        }

        public void DeleteCourse(int id)
        {
            if (id < courseRepo.ReadAll().Count())
                courseRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~Index is too big!~~~");

        }


        //TODO 4kesz
        public IEnumerable<KeyValuePair<string, int?>> CourseCleaningPrice()
        {
            var q = from x in courseRepo.ReadAll()
                    select new KeyValuePair<string, int?>(x.Title, x.Cleaner.Salary);

            return q;
        }
        public IEnumerable<KeyValuePair<string, double?>> TheMostExpensiveCourseAndTheCost() //TeacherSalaryPerCourse()
        {
            //var asd2 = courseRepo.ReadAll().AsEnumerable().GroupBy(x => x.Teacher).Select(g => new KeyValuePair<Teacher, double?>(g.Key, g.Average(x => x.Teacher.Salary))).OrderBy(x => x.Value).ToList();

            //var asd3 = from x in courseRepo.ReadAll()
            //           join s in teacherRepo.ReadAll() on x.TeacherId equals s.TeacherId
            //           let JoinedItems = new { x.Title, s.Salary }
            //           group JoinedItems by JoinedItems.Title into grp
            //           orderby grp.Average(x => x.Salary)
            //           select new KeyValuePair<string, double?>(grp.Key, grp.Average(x => x.Salary));
            ;
            //var courses = from x in courseRepo.ReadAll()
            //              group x by x.Type into g
            //              select g;

            //var teachers = from x in teacherRepo.ReadAll()
            //               select x;



            //var asd5 = courseRepo.ReadAll().Include("Teacher").AsEnumerable().GroupBy(x => x.Teacher).Select(x => new KeyValuePair<string, double?>(x.Key.LastName, x.Average(t => t.Teacher.Salary)));

            //var asd4 = from x in courses
            //           join t in teachers on x.TeacherId equals t.TeacherId
            //           let JoinedItems = new { x.Title, t.Salary }
            //           group JoinedItems by JoinedItems.Title into grp
            //           select new KeyValuePair<string, double?>(grp.Key, grp.Average(x => x.Salary));

            var asd = from x in courseRepo.ReadAll()
                      select new KeyValuePair<string, double?>(x.Title, x.Teacher.Salary);
            return asd;
        }

        public Course GetOne(int id)
        {
            return courseRepo.GetOne(id);
        }

        public IList<Course> GetAllCourses()
        {
            return courseRepo.ReadAll().ToList();
        }
    }
}
