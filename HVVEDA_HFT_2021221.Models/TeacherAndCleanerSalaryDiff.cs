using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    public class TeacherAndCleanerSalaryDiff
    {
        public Teacher teacher;
        public Cleaner cleaner;
        public override string ToString()
        {
            if (teacher.Salary - cleaner.Salary > 0)
            {
                return $"Teacher: {teacher.Firstname} {teacher.LastName} --> salary: {teacher.Salary} \n Cleaner: {cleaner.FirstName} --> salary: {cleaner.Salary} \n Difference: {teacher.Salary - cleaner.Salary}";
            }
            return $"Teacher: {teacher.Firstname} {teacher.LastName} --> salary: {teacher.Salary} \n Cleaner: {cleaner.FirstName} --> salary: {cleaner.Salary} \n Difference: {cleaner.Salary - teacher.Salary}";
        }
    }
}
