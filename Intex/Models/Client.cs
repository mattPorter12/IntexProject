using System;
using System.Collections.Generic;
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

    }
}