using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderCompounds")]
    public class OrderCompounds
    {
        [Key]
        [DisplayName("LT Number")]
        public int LTNumber { get; set; }

        [Required]
        [DisplayName("Work Order Number")]
        public int WorkOrderNum { get; set; }

        [DisplayName("Base Price")]
        public decimal BasePrice { get; set; }
    }
}