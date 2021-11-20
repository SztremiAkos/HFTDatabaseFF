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
        #endregion

        #region NON-CRUD
        Teacher GetTheYoungestTeacher();
        Cleaner GetCleanerWithTheLongestName();
        IEnumerable<CleanerNumberPerCategory> CleanerNumberPerCateg();
        IEnumerable<Teacher> GetTheDirtiestCoursesTeacher();




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
         
        //TODO noncrud 1kesz
        public IEnumerable<Teacher> GetTheDirtiestCoursesTeacher()
        {
            var dirtyC_sub = from x in courseRepo.ReadAll()
                             where x.Cleaner.Position.Equals("Fired")
                             select x;

            
            var dirtyC_SingleSub = dirtyC_sub.FirstOrDefault();

            var dirtyClass = from x in teacherRepo.ReadAll()
                             where dirtyC_SingleSub.TeacherId.Equals(x.TeacherId)
                             select x;
            return dirtyClass;
            
        }

        public void AddNewCourse(Course course)
        {
            courseRepo.AddNewCourse(course);
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

        //TODO noncrud 2kesz
        public IEnumerable<CleanerNumberPerCategory> CleanerNumberPerCateg()
        {
            var cleanergrp_sub = from x in cleanerRepo.ReadAll()
                                 group x by x.Location into g
                                 select new
                                 {
                                     cleaner = g.Key,
                                     cleaner_no = g.Count()
                                 };
            var cleanergrp = from x in courseRepo.ReadAll()
                             join z in cleanergrp_sub on x.CourseID equals z.cleaner.CourseID
                             let joinedItem = new { x.CourseID, x.Type, z.cleaner_no }
                             group joinedItem by joinedItem.Type into grp
                             select new CleanerNumberPerCategory
                             {
                                 Location = grp.Key,
                                 CleanerCount = grp.Sum(x => x.cleaner_no)


                             };
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

    }
}
