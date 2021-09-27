using DemoAssessmentAPI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoAssessment.Domain
{
    [Table("Model", Schema = "dbo")]
    public class Model
    {
        [Key]
        public int ModelId { get; set; }
        public int ManufacturerId { get; set; }
        public string ModelName { get; set; }
        public DateTime FirstProductionDate { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        [ForeignKey("ManufacturerId")]
        public virtual Manufacturer Manufacturer { get; set; }
        [NotMapped]
        public string ManufacturerName { get; set; }


    }
}
