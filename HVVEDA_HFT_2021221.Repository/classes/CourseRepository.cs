using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    class CourseRepository : ICourseRepository
    {
        public void ChangeCreditAmount(int id, int newCreditAmount)
        {
            throw new NotImplementedException();
        }

        public void ChangeLocation(int id, string NewTitle)
        {
            throw new NotImplementedException();
        }

        public void ChangeTitle(int id, string NewTitle)
        {
            throw new NotImplementedException();
        }

        public Course DeleteOne(int id)
        {
            throw new NotImplementedException();
        }

        public Course GetOne(int id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Course> ReadAll()
        {
            throw new NotImplementedException();
        }
    }
}
