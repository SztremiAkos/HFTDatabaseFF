using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Logic
{
    public interface IStudentLogic
    {
        ICollection<Course> GetAllCourses(int id);
        void AddNewStudent(Student student);
        Student GetStudentbyId(int id);
        void DelStudentbyId(int id);

        IList<Student> GetAllStudents();

        public void UpdateStudent(Student student);
        public IEnumerable<KeyValuePair<string, int>> StudentCountPerCategory();


    }
    public class StudentLogic : IStudentLogic
    {
        IStudentRepository studentRepo;
        ICourseRepository courseRepo;

        public StudentLogic(IStudentRepository studRepo, ICourseRepository courseRepo)
        {
            this.studentRepo = studRepo;
            this.courseRepo = courseRepo;
        }

        public void AddNewStudent(Student student)
        {
            if (student.Firstname == null || student.Firstname == "")
            {
                throw new NullReferenceException("Name cant be null!");
            }
            studentRepo.AddNewStudent(student);

        }

        public void DelStudentbyId(int id)
        {
            if (id <= studentRepo.ReadAll().Max(t => t.StudentID))
                studentRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public ICollection<Course> GetAllCourses(int id)
        {
            if (id <= studentRepo.ReadAll().Max(t => t.StudentID))
                return studentRepo.GetAllCourses(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public IList<Student> GetAllStudents()
        {
            return studentRepo.ReadAll().ToList();
        }

        public Student GetStudentbyId(int id)
        {
            if (id <= studentRepo.ReadAll().Max(t => t.StudentID))
                return studentRepo.GetOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public void UpdateStudent(Student student)
        {
            studentRepo.UpdateStudent(student);
        }
        public IEnumerable<KeyValuePair<string,int>> StudentCountPerCategory()
        {
            var StudPerClass = from x in courseRepo.ReadAll()
                               group x by x.Type into g
                               select new KeyValuePair<string, int>(g.Key, g.Count());
            ;

            return StudPerClass;
        }
    }
}
