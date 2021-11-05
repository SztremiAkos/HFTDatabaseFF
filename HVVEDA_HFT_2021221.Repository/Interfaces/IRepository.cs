using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    public interface IRepository<T> where T : class
    {
        ///CRUD
        ///create
        ///read -ok
        ///update
        ///delete - ok
        
        //Read
        T GetOne(int id);
        IQueryable<T> ReadAll();

        //delete
        T DeleteOne(int id);

    }
}
