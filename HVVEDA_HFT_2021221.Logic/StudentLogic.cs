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
        //TODO noncrud 3kesz
        public IEnumerable<StudentcountPerClassRoom> StudentCountPerClassRoom()
        {
            var studPerClass_sub = from x in courseRepo.ReadAll()
                                   group x by x.Location into g
                                   select new
                                   {
                                       LOCATION = g.Key,
                                       Class_Number = g.Count()
                                   };
            var StudPerClass = from x in courseRepo.ReadAll()
                               join z in studPerClass_sub on x.Location equals z.LOCATION
                               let joinedItem = new { x.Location, x.CourseID, z.Class_Number }
                               group joinedItem by joinedItem.Location into grp
                               select new StudentcountPerClassRoom
                               {
                                   Location = grp.Key,
                                   StudentNumber = grp.Sum(x => x.Class_Number)
                               };
            return StudPerClass;
        }
    }
}
