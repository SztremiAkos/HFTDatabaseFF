using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Models
{
    public class CleanerNumberPerCategory
    {
        public string Location;
        public int? CleanerCount;
        public override string ToString()
        {
            return @"\t { "+Location+" }  -- { "+CleanerCount+" }";
        }

    }
}
