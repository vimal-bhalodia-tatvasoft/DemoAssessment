using DemoAssessment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public interface IVehicleService
    {
        List<Vehicle> GetVehicles(int Id = 0);
        bool AddVehicle(Vehicle model);
        bool UpdateVehicle(Vehicle model);
        bool DeleteVehicle(Vehicle model);
        List<Vehicle> GetVehiclesbyModelId(int ModelId);
    }
}
