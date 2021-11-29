using HVVEDA_HFT_2021221.Data;
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


        public void AddNewCleaner(Cleaner cleaner)
        {
            ctx.Add(cleaner);
            ctx.SaveChanges();
        }  //c
        public override void DeleteOne(int id)
        {
            var cleaner = GetOne(id);
            var course = cleaner.Location;
            course.Cleaner = null;
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        } //d
        public override Cleaner GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.CleanerId == id);
        } //r
        public void ChangeCourse(int id, Course newCourse)
        {
            var toChange = GetOne(id);
            toChange.Location = newCourse;
            ctx.SaveChanges();
        }  //u
        public void ChangePosition(int id, string newPosition)
        {
            var toChange = GetOne(id);
            toChange.Position = newPosition;
            ctx.SaveChanges();
        }
        public void UpdateCleaner(Cleaner cleaner)
        {
            var toUpdate = GetOne(cleaner.CleanerId);
            toUpdate.Position = cleaner.Position;
            toUpdate.Salary = cleaner.Salary;
            ;
            ctx.SaveChanges();
        }
        public void SetNewSalary(int id, int newAmount)
        {
            var toChange = GetOne(id);
            toChange.Salary = newAmount;
            ctx.SaveChanges();
        }
    }
}
