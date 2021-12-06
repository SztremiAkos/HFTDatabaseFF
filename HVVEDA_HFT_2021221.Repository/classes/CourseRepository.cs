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
    public class CourseRepository :Repository<Course>,ICourseRepository
    {
        public CourseRepository(CourseDbContext ctx) : base(ctx) { }

        public void AddNewCourse(Course course)
        {
            ctx.Add(course);
            ctx.SaveChanges();
        }
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

        public void UpdateCourse(Course course)
        {
            var toUpdate = GetOne(course.CourseID);
            toUpdate.Credits = course.Credits;
            toUpdate.Length = course.Length;
            toUpdate.Location = course.Location;
            toUpdate.Title = course.Title;
            ctx.SaveChanges();
        }
    }
}
