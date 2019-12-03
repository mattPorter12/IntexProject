using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Compound")]
    public class Compound
    {

        [Key]
        public int LTNumber { get; set; }
        [Key]
        public int SequenceCode { get; set; }

        public int AssayId { get; set; }

        public int PriorityNumber { get; set; }

        [Required]
        [DisplayName("Compound Name")]
        public string CompName { get; set; }

        [Required]
        [DisplayName("Compound Quantity")]
        public double CompQuantity { get; set; }

        [Required]
        [DisplayName("Arrival Date")]
        [RegularExpression(@"^\d\d\\\d\d\\\d\d\d\d$", ErrorMessage = "Should be MM/DD/YYYY")]
        public string ArrivalDate { get; set; }

        [Required]
        [DisplayName("Received By")]
        public string ReceivedBy { get; set; }

        [Required]
        public int EmployeeID { get; set; }

        [Required]
        [DisplayName("Due Date")]
        [RegularExpression(@"^\d\d\\\d\d\\\d\d\d\d$", ErrorMessage = "Should be MM/DD/YYYY")]
        public string DueDate { get; set; }

        [Required]
        [DisplayName("Compound Appearance")]
        public string CompAppearance { get; set; }

        [Required]
        [DisplayName("Compound Client Weight")]
        public string CompClientWeight { get; set; }
    }
}