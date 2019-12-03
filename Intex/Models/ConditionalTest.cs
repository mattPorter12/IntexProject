using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("ConditionalTest")]
    public class CondtionalTest
    {
        [Key]
        public int TestID { get; set; }
        
        public int AssayID { get; set; }

        [Required]
        [DisplayName("Test Name")]
        public string TestName { get; set; }


        [Required]
        [DisplayName("Test Protocol")]
        public string TestProtocol { get; set; }


    }
}