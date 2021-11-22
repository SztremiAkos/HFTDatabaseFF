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
        public IEnumerable<StudentcountPerClassRoom> StudentCountPerClassRoom();


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
            if (student.Firstname ==null || student.Firstname =="")
            {
                throw new NullReferenceException("Name cant be null!");
            }
            studentRepo.AddNewStudent(student);
            
        }

        public void DelStudentbyId(int id)
        {
            if (id < studentRepo.ReadAll().Count())
                studentRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public ICollection<Course> GetAllCourses(int id)
        {
            if (id < studentRepo.ReadAll().Count())
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
            if (id < studentRepo.ReadAll().Count())
                return studentRepo.GetOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public void UpdateStudent(Student student)
        {
            studentRepo.UpdateStudent(student);
        }
        public IEnumerable<StudentcountPerClassRoom> StudentCountPerClassRoom()
        {
            var StudPerClass = from x in courseRepo.ReadAll()
                                   group x by x.Location into g
                                   select new StudentcountPerClassRoom
                                   {
                                       Location = g.Key,
                                       StudentNumber = g.Count()
                                   };
            return StudPerClass;
        }
    }
}
