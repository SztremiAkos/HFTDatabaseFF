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
        #endregion

        #region NON-CRUD
        Teacher GetTheYoungestTeacher();
        Cleaner GetCleanerWithTheLongestName();
        IEnumerable<StudentNumberPerCategory> StudentNumberPerCategories();




        #endregion
    }
    public class CourseLogic : ICourseLogic
    {
        ICourseRepository courseRepo;
        ITeacherRepository teacherRepo;
        ICleanerRepository cleanerRepo;

        public CourseLogic(ICourseRepository courseRepo, ITeacherRepository teacherRepo, ICleanerRepository cleanerRepo)
        {
            this.courseRepo = courseRepo;
            this.teacherRepo = teacherRepo;
            this.cleanerRepo = cleanerRepo;
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

        public IEnumerable<StudentNumberPerCategory> StudentNumberPerCategories()
        {
            return courseRepo.ReadAll().GroupBy(x => x.Type).Select(x => new StudentNumberPerCategory { Category = x.Key, StudentCount = x.Count() });
        }

        public void UpdateCourse(Course course)
        {
            courseRepo.UpdateCourse(course);
        }
    }
}
