using DemoAssessmentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public interface IManufacturerService
    {
        List<Manufacturer> GetManufacturers(int Id=0);
        bool AddManufacturer(Manufacturer model);
        bool UpdateManufacturer(Manufacturer model);
        bool DeleteManufacturer(Manufacturer model);
    }
}
