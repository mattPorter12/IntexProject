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
        public double CompClientWeight { get; set; }

        [Required]
        [DisplayName("Compound Mole Mass")]
        public double CompMoleMass { get; set; }

        [Required]
        [DisplayName("Compound MTD")]
        public double CompMTD { get; set; }

        [Required]
        [DisplayName("Compound Actual Weight")]
        public double CompActualWeight { get; set; }

        [Required]
        [DisplayName("Compound Concentration")]
        public double CompConcentration { get; set; }

        [Required]
        [DisplayName("Active Status")]
        public Boolean IsActive { get; set; }

        [Required]
        public int CompStatusId { get; set; }

        //quantResults- file
        //qualtResults- file

        [Required]
        public string UsableResults { get; set; }
    }
}