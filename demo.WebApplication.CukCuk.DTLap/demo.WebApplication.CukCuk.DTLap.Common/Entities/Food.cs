using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace demo.WebApplication.CukCuk.DTLap.Common.Entities
{
    public class Food
    {
        [Key]
        [Required]
        public Guid FoodID { get; set; }

        [Required]
        public string FoodName { get; set; }

        [Required]
        public string FoodCode { get; set; }

        public string MenuGroup { get; set; }

        public string FoodType { get; set; }

        [Required]
        public string CaculationUnit { get; set; }

        public long CostPrice { get; set; }

        [Required]
        public long SellingPrice { get; set; }

        public bool ChangeOverTime { get; set; }

        public bool OnFreeChange { get; set; }

        public bool Quantification { get; set; }

        public bool ShowOnMenu { get; set; }

        public bool StopSelling { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }

        public DateTime ModifiedDate { get; set; }

        public string ModifiedBy { get; set;}

        public Guid? ImageID { get; set; }

        public string Description { get; set; }

        public string ProcessedAt { get; set; }
    }
}
