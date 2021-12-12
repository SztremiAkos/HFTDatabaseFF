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
        IEnumerable<KeyValuePair<string, double?>> TeacherSalaryPerCourse();//TeacherSalaryPerCourse();




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
        //TODO GetTheDirtiestCoursesTeacher //TOBB //done
        public IEnumerable<Teacher> GetTheDirtiestCoursesTeacher()
        {
            var dirtyC_sub = from x in courseRepo.ReadAll()
                             where x.Cleaner.Position == "Fired"
                             select x;

            var dirtyC_SingleSub = dirtyC_sub.FirstOrDefault();

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
        //TODO CleanerNumberPerClassroom //TOBB //done
        public IEnumerable<KeyValuePair<string, int?>> CleanerNumberPerClassroom()
        {
            List<Course> courses = new List<Course>();
            List<Cleaner> cleaners = new List<Cleaner>();
            foreach (var item in courseRepo.ReadAll())
            {
                if (item.CleanerId!=null)
                {
                    courses.Add(item);
                }
            }
            foreach (var item in cleanerRepo.ReadAll())
            {
                cleaners.Add(item);
            }
            return from x in cleaners
                   join c in courses on x.CleanerId equals c.CleanerId
                   let joinedItems = new { x.CleanerId, c.Location, x.Salary }
                   group joinedItems by joinedItems.Location into g
                   select new KeyValuePair<string, int?>(g.Key, g.Count());
        }

        public void UpdateCourse(Course course)
        {
            courseRepo.UpdateCourse(course);
        }

        public void DeleteCourse(int id)
        {
            if (id <= courseRepo.ReadAll().Max(t => t.CourseID))
                courseRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~Index is too big!~~~");

        }


        //TODO COURSECLEANINGPRICE //TOBB //helyes
        public IEnumerable<KeyValuePair<string, int?>> CourseCleaningPrice()
        {
            List<Cleaner> cleaners = new List<Cleaner>();
            List<Course> courses = new List<Course>();
            var cleanerswithcourse = from x in cleanerRepo.ReadAll() //cleaner
                                     where x.Location != null && x.Salary != 0
                                     select x;
            foreach (var item in cleanerswithcourse)
            {
                cleaners.Add(item);
            }
            foreach (var item in courseRepo.ReadAll())
            {
                courses.Add(item);
            }
            return from x in courses
                   join c in cleaners on x.CleanerId equals c.CleanerId
                   select new KeyValuePair<string, int?>(x.Title, c.Salary);
        }

        //TODO TeacherSalaryPerCourse //TOBB //helyes
        public IEnumerable<KeyValuePair<string, double?>> TeacherSalaryPerCourse()
        {
            List<Course> courses = new List<Course>();
            List<Teacher> teachers = new List<Teacher>();
            foreach (var item in courseRepo.ReadAll())
            {
                courses.Add(item);
            }


            var teacherswithcourse = from x in teacherRepo.ReadAll()
                                     where x.Courses.Count != 0
                                     select x;
            foreach (var item in teacherswithcourse)
            {
                teachers.Add(item);
            }
            var q = from x in courses
                    join t in teacherswithcourse on x.TeacherId equals t.TeacherId
                    select new KeyValuePair<string, double?>(x.Title, t.Salary);
            return q;
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
