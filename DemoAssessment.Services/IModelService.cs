using DemoAssessment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Services
{
    public interface IModelService
    {
        List<Model> GetModels(int Id = 0);
        bool AddModel(Model model);
        bool UpdateModel(Model model);
        bool DeleteModel(Model model);
        List<Model> GetModelsbyManufacturerId(int ManufacturerId);
    }
}
