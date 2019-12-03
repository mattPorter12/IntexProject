using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("PaymentInfo")]
    public class PaymentInfo
    {
        [Key]
        [Required]
        [RegularExpression(@"^\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d\d$", ErrorMessage = "Card Number should be 16 digits")]
        [DisplayName("Card Number")]
        public int CardNumber { get; set; }

        [Required]
        [RegularExpression(@"^\d\d\\\d\d$", ErrorMessage = "Format should be MM/YY")]
        [DisplayName("Expiration Date")]
        public string ExpirationDate { get; set; }

        [Required]
        [RegularExpression(@"^\d\d\d")]
        [DisplayName("Security Number")]
        public int SecurityNumber { get; set; }
    }
}