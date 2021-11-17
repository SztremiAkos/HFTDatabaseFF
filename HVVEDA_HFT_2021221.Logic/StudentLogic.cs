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
    }
    public class StudentLogic : IStudentLogic
    {
        IStudentRepository studrepo;

        public StudentLogic(IStudentRepository studrepo)
        {
            this.studrepo = studrepo;
        }

        public void AddNewStudent(Student student)
        {
            studrepo.AddNewStudent(student);
        }

        public void DelStudentbyId(int id)
        {
            if (id < studrepo.ReadAll().Count())
                studrepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public ICollection<Course> GetAllCourses(int id)
        {
            if (id < studrepo.ReadAll().Count())
                return studrepo.GetAllCourses(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public IList<Student> GetAllStudents()
        {
            return studrepo.ReadAll().ToList();
        }

        public Student GetStudentbyId(int id)
        {
            if (id < studrepo.ReadAll().Count())
                return studrepo.GetOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index is too big!~~~~");
        }

        public void UpdateStudent(Student student)
        {
            studrepo.UpdateStudent(student);
        }
    }
}
