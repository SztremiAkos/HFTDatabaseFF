﻿using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Logic
{
    public interface ITeacherLogic
    {
        void ChangeSalary(int id, int newsalary);
        ICollection<Course> GetCourses(int id);
        void AddNewTeacher(Teacher teacher);

        void UpdateTeacher(Teacher teacher);
        void DeleteTeacher(int id);


    }
    public class TeacherLogic : ITeacherLogic
    {
        ITeacherRepository teacherRepo;

        public TeacherLogic(ITeacherRepository teacherRepo)
        {
            this.teacherRepo = teacherRepo;
        }

        public void AddNewTeacher(Teacher teacher)
        {
            teacherRepo.AddNewTeacher(teacher);
        }

        public void ChangeSalary(int id, int newsalary)

        {
            if (id < teacherRepo.ReadAll().Count())
                teacherRepo.ChangeSalary(id, newsalary);
            else
                throw new IndexOutOfRangeException("~~~Index is too big!~~~");

        }

        public void DeleteTeacher(int id)
        {
            if (id < teacherRepo.ReadAll().Count())
                teacherRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~Index is too big!~~~");

        }

        public ICollection<Course> GetCourses(int id)
        {
            if (id < teacherRepo.ReadAll().Count())
                return teacherRepo.GetCourses(id);
            else
                throw new IndexOutOfRangeException("~~~Index is too big!~~~");

        }

        public void UpdateTeacher(Teacher teacher)
        {
            teacherRepo.UpdateTeacher(teacher);
        }
    }
}
