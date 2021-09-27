using DemoAssessment.Domain;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public class VehicleService : IVehicleService
    {
        #region Fields

        private DemoDbContext _dbContext;

        #endregion

        #region Ctor

        public VehicleService()
        {
            _dbContext = new DemoDbContext();
        }

        #endregion

        #region Methods
        public List<Vehicle> GetVehicles(int Id = 0)
        {
            if (Id == 0)
                return _dbContext.Vehicles.ToList();
            else
                return _dbContext.Vehicles.Where(x => x.VehicleId == Id).ToList();
        }

        public bool AddVehicle(Vehicle model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Vehicle");

                _dbContext.Entry(model).State = EntityState.Added;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool UpdateVehicle(Vehicle model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Vehicle");

                _dbContext.Entry(model).State = EntityState.Modified;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }


        public bool DeleteVehicle(Vehicle model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Vehicle");

                _dbContext.Entry(model).State = EntityState.Deleted;
                _dbContext.SaveChanges();
            }
            catch (Exception e)
            {
                return false;
            }
            return true;

        }
        public List<Vehicle> GetVehiclesbyModelId(int ModelId)
        {

            return _dbContext.Vehicles.Where(x => x.ModelId == ModelId).ToList();
        }


        #endregion
    }
}
