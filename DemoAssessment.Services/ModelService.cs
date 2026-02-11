using DemoAssessment.Domain;
using Identity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;

using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public class ModelService : IModelService
    {
        #region Fields

        private DemoDbContext _dbContext;

        #endregion

        #region Ctor

        public ModelService()
        {
            _dbContext = new DemoDbContext();
        }

        #endregion

        #region Methods
        public List<Model> GetModels(int Id = 0)
        {
            if (Id == 0)
                return _dbContext.Models.ToList();
            else
                return _dbContext.Models.Where(x => x.ModelId == Id).ToList();
        }

        public bool AddModel(Model model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Model");

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
        public bool UpdateModel(Model model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Model");

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


        public bool DeleteModel(Model model)
        {
            try
            {
                if (model == null)
                    throw new ArgumentNullException("Model");

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

        public List<Model> GetModelsbyManufacturerId(int ManufacturerId)
        {
            return _dbContext.Models.Where(x => x.ManufacturerId == ManufacturerId).ToList();
        }


        #endregion
    }
}
