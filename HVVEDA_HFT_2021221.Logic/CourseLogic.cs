using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
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
        Course GetOne(int id);
        #endregion

        #region NON-CRUD
        Teacher GetTheYoungestTeacher();
        Cleaner GetCleanerWithTheLongestName();
        IEnumerable<CleanerNumberPerCategory> CleanerNumberPerCateg();
        IEnumerable<Teacher> GetTheDirtiestCoursesTeacher();

        IEnumerable<KeyValuePair<string, int?>> CourseCleaningPrice();
        IEnumerable<KeyValuePair<Teacher, double?>> TeacherSalaryPerCourse();




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

        public Cleaner GetCleanerWithTheLongestName()
        {
            var maxLength = cleanerRepo.ReadAll().Max(x => x.Name.Length);
            return cleanerRepo.ReadAll().Where(x => x.Name.Length == maxLength).FirstOrDefault();
        }

        public Teacher GetTheYoungestTeacher()
        {
            var minAge = teacherRepo.ReadAll().Min(x => x.Age);
            return teacherRepo.ReadAll().Where(x => x.Age == minAge).FirstOrDefault();
        }

        public IEnumerable<CleanerNumberPerCategory> CleanerNumberPerCateg()
        {
            var cleanergrp_sub = from x in cleanerRepo.ReadAll()
                                 group x by x.Location.Location into g
                                 select new
                                 {
                                     location = g.Key,
                                     cleaner_no = g.Count()
                                 };
            var cleanergrp = from x in courseRepo.ReadAll()
                             join z in cleanergrp_sub on x.Location equals z.location
                             let joinedItem = new { x.CourseID, x.Type, z.cleaner_no }
                             group joinedItem by joinedItem.Type into grp
                             select new CleanerNumberPerCategory
                             {
                                 Location = grp.Key,
                                 CleanerCount = grp.Count()


                             };
            ;
            return cleanergrp;
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
            return from x in courseRepo.ReadAll()
                   group x by x.Title into g
                   select new KeyValuePair<string, int?>(g.Key, g.Sum(x => x.Cleaner.Salary));

        }
        public IEnumerable<KeyValuePair<Teacher, double?>> TeacherSalaryPerCourse()
        {
            var asd = from x in courseRepo.ReadAll()
                      group x by x.Teacher into g
                      select new KeyValuePair<Teacher, double?>(g.Key, g.Average(x => x.Teacher.Salary));
            ;
            return asd;
        }

        public Course GetOne(int id)
        {
            return courseRepo.GetOne(id);
        }
    }
}
