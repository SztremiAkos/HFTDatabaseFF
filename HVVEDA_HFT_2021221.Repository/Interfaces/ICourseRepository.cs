using HVVEDA_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Repository
{
    interface ICourseRepository
    {
        void ChangeCreditAmount(int id, int newCreditAmount);
        void ChangeTitle(int id, string NewTitle);
        void ChangeLocation(int id, string NewTitle);
    }
}
