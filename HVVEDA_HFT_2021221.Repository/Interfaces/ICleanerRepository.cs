using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public interface ICleanerRepository
    {
        void ChangeCourse(int id, Course newCourse);
        void ChangePosition(int id, string newPosition);
        void SetNewSalary(int id, int newAmount);
    }
}
