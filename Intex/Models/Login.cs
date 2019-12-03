using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Login")]
    public class Login
    {
        [Key]
        [Required]
        [DisplayName("User Name")]
        public string UserName { get; set; }

        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }

        public string ClientID { get; set; }
    }
}