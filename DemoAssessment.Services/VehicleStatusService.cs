using DemoAssessment.Domain;
using Identity.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public class VehicleStatusService : IVehicleStatusService
    {
        #region Fields

        private DemoDbContext _dbContext;

        #endregion

        #region Ctor

        public VehicleStatusService()
        {
            _dbContext = new DemoDbContext();
        }

        #endregion

        #region Methods

        public List<VehicleStatus> GetVehicleStatuses()
        {
            return _dbContext.VehiclesStatuses.ToList();

        }
        #endregion
    }
}
