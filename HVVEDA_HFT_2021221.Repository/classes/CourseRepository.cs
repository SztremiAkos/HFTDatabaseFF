using HVVEDA_HFT_2021221.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    class CourseRepository : Repository<Course>,ICourseRepository
    {
        public CourseRepository(DbContext ctx) : base(ctx) { }

        public override void DeleteOne(int id)
        {
            ctx.Remove(GetOne(id));
            ctx.SaveChanges();
        }

        public void ChangeCreditAmount(int id, int newCreditAmount)
        {
            var toChange = GetOne(id);
            toChange.Credits = newCreditAmount;
            ctx.SaveChanges();
        }

        public void ChangeLocation(int id, string NewTitle)
        {
            var toChange = GetOne(id);
            toChange.Location = NewTitle;
            ctx.SaveChanges();
        }

        public void ChangeTitle(int id, string NewTitle)
        {
            var toChange = GetOne(id);
            toChange.Title = NewTitle;
            ctx.SaveChanges();
        }

        public override Course GetOne(int id)
        {
            return ReadAll().SingleOrDefault(x => x.CourseID == id);
        }
    }
}
