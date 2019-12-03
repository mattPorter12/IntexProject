using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{   
    [Table("Client")]
    public class Client
    {
        
        [Key]
        public int ClientID { get; set; }

        [Required]
        [Display(Name = "Company Name")]
        public string ClientName { get; set; }

        [Required]
        [Display(Name = "Address 1")]
        public string PhysAddress1 { get; set; }

        [Required]
        [DisplayName("Address 2")]
        public string PhysAddress2 { get; set; }

        [Required]
        [DisplayName("City")]
        public string PhysCity { get; set; }

        [Required]
        [DisplayName("State")]
        public string PhysState { get; set; }

        [Required]
        [RegularExpression(@"^\d\d\d\d\d$", ErrorMessage = "Zip Code should be five digits")]
        [DisplayName("Zip Code")]
        public string PhysZipCode { get; set; }

        [Required]
        [DisplayName("Person to Contact")]
        public string PointOfContact { get; set; }

        [Required]
        [RegularExpression(@"^\(\d\d\d\) \d\d\d-\d\d\d\d$", ErrorMessage = "Please display phone number in this format: (000) 000-0000")]
        [DisplayName("Phone Number")]
        public string PointPhoneNum { get; set; }

        [DisplayName("Discount Rate")]
        public decimal DiscountRate { get; set; }

        [DisplayName("Account Balance")]
        public decimal Balance { get; set; }
    }
}