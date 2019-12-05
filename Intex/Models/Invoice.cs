using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Invoice")]
    public class Invoice
    {
        [Key]
        public int InvoiceID { get; set; }

        [Required]
        [DisplayName("Total Cost")]
        public decimal TotalMatCost { get; set; }

        [DisplayName("Client ID Number")]
        public int ClientID { get; set; }

        [Required]
        [DisplayName("Due Date")]
        [RegularExpression(@"^\d\d\/d\d\/d\d\d\d$", ErrorMessage = "Should be MM/DD/YYYY")]
        public string DueDate { get; set; }

        [Required]
        
        [DisplayName("Early Date")]
        [RegularExpression(@"^\d\d\/d\d\/d\d\d\d$", ErrorMessage = "Should be MM/DD/YYYY")]
        public string EarlyDate { get; set; }

        [Required]
        [DisplayName("Early Discount Price")]
        public decimal EarlyDiscount { get; set; }

        public decimal SubTotal { get; set; }
    }
}