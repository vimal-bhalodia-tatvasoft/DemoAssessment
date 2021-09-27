using DemoAssessment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public interface IVehicleStatusService
    {
        List<VehicleStatus> GetVehicleStatuses();
    }
}
