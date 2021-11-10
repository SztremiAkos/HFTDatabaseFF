using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public class CleanerRepository : Repository<Cleaner>, ICleanerRepository
    {
        public CleanerRepository(DbContext ctx) : base(ctx) { }

        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public override Cleaner GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.CleanerId == id);
        }

        void ICleanerRepository.ChangeCourse(int id, Course newCourse)
        {
            var toChange = GetOne(id);
            toChange.Location = newCourse;
            ctx.SaveChanges();
        }

        void ICleanerRepository.ChangePosition(int id, string newPosition)
        {
            var toChange = GetOne(id);
            toChange.Position = newPosition;
            ctx.SaveChanges();
        }

        void ICleanerRepository.SetNewSalary(int id, int newAmount)
        {
            var toChange = GetOne(id);
            toChange.Salary = newAmount;
            ctx.SaveChanges();
        }
    }
}
