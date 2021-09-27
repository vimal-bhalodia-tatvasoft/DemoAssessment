using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Models
{
    public class VehicleModel
    {
        public int VehicleId { get; set; }
        [Display(Name = "Model")]
        [Required(ErrorMessage = "Please select Model")]
        public int ModelId { get; set; }
        [Display(Name = "Vehicle Status")]
        [Required(ErrorMessage = "Please select Vehicle status")]
        public int StatusId { get; set; }
        public string Year { get; set; }
        public string Color { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public string ModelName { get; set; }
        public string StatusName { get; set; }
        public List<SelectListItem> ModelList { get; set; }
        public List<SelectListItem> StatusList { get; set; }
    }
}
