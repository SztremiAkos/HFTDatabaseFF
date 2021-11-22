using HVVEDA_HFT_2021221.Models;
using HVVEDA_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HVVEDA_HFT_2021221.Logic
{
    public interface ICleanerLogic
    {
        #region crud
        void AddNewCleaner(Cleaner cleaner);
        void DeleteCleaner(int id);
        Cleaner GetCleanerById(int id);
        void ChangeCourse(int id, Course newCourse);
        void ChangePosition(int id, string newPosition);

        void UpdateCleaner(Cleaner cleaner);
        void SetNewSalary(int id, int newAmount);
        IList<Cleaner> ReadAll();

        #endregion

        #region noncrud

        int? GetLeastSalary();
        IList<Cleaner> FirstFloorWorkers();
        #endregion
    }
    public class CleanerLogic : ICleanerLogic
    {

        public CleanerLogic(ICleanerRepository cleanerLogic)
        {
            this.cleanerRepo = cleanerLogic;
        }

        private ICleanerRepository cleanerRepo;


        public void AddNewCleaner(Cleaner cleaner)
        {
            cleanerRepo.AddNewCleaner(cleaner);
        }

        public void ChangeCourse(int id, Course newCourse)
        {


            if (id < cleanerRepo.ReadAll().Count())
                cleanerRepo.ChangeCourse(id, newCourse);
            else
                throw new IndexOutOfRangeException("~~~~Index Is too Big!~~~~");
        }

        public void ChangePosition(int id, string newPosition)
        {
            if (id < cleanerRepo.ReadAll().Count())
                cleanerRepo.ChangePosition(id, newPosition);
            else
                throw new IndexOutOfRangeException("~~~~Index Is too Big!~~~~");

        }

        public void DeleteCleaner(int id)
        {
            if (id < cleanerRepo.ReadAll().Count())
                cleanerRepo.DeleteOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index Is too Big!~~~~");

        }

        public IList<Cleaner> FirstFloorWorkers()
        {
            return cleanerRepo.ReadAll().Where(x => x.Location.ToString().ToUpper().Contains('F')).ToList();
        }

        public IList<Cleaner> ReadAll()
        {
            return cleanerRepo.ReadAll().ToList();
        }

        public Cleaner GetCleanerById(int id)
        {
            if (id < cleanerRepo.ReadAll().Count())
                return cleanerRepo.GetOne(id);
            else
                throw new IndexOutOfRangeException("~~~~Index Is too Big!~~~~");
        }

        public int? GetLeastSalary()
        {
            return cleanerRepo.ReadAll().OrderByDescending(x => x.Salary).FirstOrDefault().Salary;
        }



        public void SetNewSalary(int id, int newAmount)
        {
            if (id < cleanerRepo.ReadAll().Count())
                cleanerRepo.SetNewSalary(id, newAmount);
            else
                throw new IndexOutOfRangeException("~~~~Index Is too Big!~~~~");
        }

        public void UpdateCleaner(Cleaner cleaner)
        {
            cleanerRepo.UpdateCleaner(cleaner);
        }
    }
}
