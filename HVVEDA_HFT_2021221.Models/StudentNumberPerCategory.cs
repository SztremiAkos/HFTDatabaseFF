using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    public class StudentNumberPerCategory
    {
        public string Category;
        public int? StudentCount;
        public override string ToString()
        {
            return @"\t { "+Category+" }  -- { "+StudentCount+" }";
        }

    }
}
