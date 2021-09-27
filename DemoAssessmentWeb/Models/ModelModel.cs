using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DemoAssessmentWeb.Models
{
    public class ModelModel
    {
        public int ModelId { get; set; }
        [Display(Name = "Manufacturer")]
        [Required(ErrorMessage = "Please select Manufacturer")]
        public int ManufacturerId { get; set; }
        [Display(Name = "Model Name")]
        [Required(ErrorMessage = "Please enter model name")]
        [StringLength(200, ErrorMessage = "Please enter maximum 100 length of characters in name.")]
        public string ModelName { get; set; }
        public DateTime FirstProductionDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<SelectListItem> ManufacturerList { get; set; }
    }
}
