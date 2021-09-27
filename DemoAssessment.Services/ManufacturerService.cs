using DemoAssessmentAPI.Models;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public class ManufacturerService : IManufacturerService
    {
        #region Fields

        private DemoDbContext _dbContext;

        #endregion

        #region Ctor

        public ManufacturerService()
        {
            _dbContext = new DemoDbContext();
        }

        #endregion

        #region Methods
        public List<Manufacturer> GetManufacturers(int Id = 0)
        {
            if (Id == 0)
                return _dbContext.Manufacturers.ToList();
            else
                return _dbContext.Manufacturers.Where(x => x.ManufacturerId == Id).ToList();
        }

        public bool AddManufacturer(Manufacturer model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Manufacturer");

                _dbContext.Entry(model).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
            return true;
        }
        public bool UpdateManufacturer(Manufacturer model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Manufacturer");

                _dbContext.Entry(model).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
            return true;

        }


        public bool DeleteManufacturer(Manufacturer model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Manufacturer");

                _dbContext.Entry(model).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                throw e;
                return false;
            }
            return true;

        }


        #endregion
    }
}
