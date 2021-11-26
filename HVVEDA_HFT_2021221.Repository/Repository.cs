using HVVEDA_HFT_2021221.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected DbContext ctx;
        public Repository(DbContext ctx)
        {
            this.ctx = ctx;
        }

        public abstract void DeleteOne(int id); // cant implement here

        public abstract T GetOne(int id); //cant implement here

        public IQueryable<T> ReadAll()
        {
            return ctx.Set<T>(); //returns all values of the T type
        }
    }
}
