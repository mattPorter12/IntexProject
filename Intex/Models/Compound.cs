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

        [Required]
        [DisplayName("Assay Type")]
        public int AssayId { get; set; }

        [Required]
        [DisplayName("Priority")]
        public int PriorityNumber { get; set; }

        [Required]
        [DisplayName("Compound Name")]
        public string CompName { get; set; }

        [Required]
        [DisplayName("Compound Quantity")]
        public decimal? CompQuantity { get; set; }

        
        [DisplayName("Arrival Date")]
        public DateTime? ArrivalDate { get; set; }

        [DisplayName("Received By")]
        public string ReceivedBy { get; set; }

        public int? EmployeeID { get; set; }

        [Required]
        [DisplayName("Due Date")]
        //[RegularExpression(@"^\d\d/\d\d/\d\d\d\d$", ErrorMessage = "Should be MM/DD/YYYY")]
        public DateTime DueDate { get; set; }

        [DisplayName("Compound Appearance")]
        public string CompAppearance { get; set; }

        [Required]
        [DisplayName("Compound Client Weight")]
        public decimal? CompClientWeight { get; set; }

        [DisplayName("Compound Mole Mass")]
        public decimal? CompMoleMass { get; set; }

        [DisplayName("Compound MTD")]
        public decimal? CompMTD { get; set; }

        [DisplayName("Compound Actual Weight")]
        public decimal? CompActualWeight { get; set; }

        [DisplayName("Compound Concentration")]
        public decimal? CompConcentration { get; set; }

        [DisplayName("Active Status")]
        public string IsActive { get; set; }

        public int? CompStatusID { get; set; }

        //quantResults- file
        //qualtResults- file

        public string UsableResults { get; set; }
    }
}