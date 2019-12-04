using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Assay")]
    public class Assay
    {
        [Key]
        public int AssayID { get; set; }

        [Required]
        [DisplayName("Assay Name")]
        public string AssayName { get; set; }

        [Required]
        [DisplayName("Assay Type")]
        public string AssayType { get; set; }

        [Required]
        [DisplayName("Assay Protocol")]
        public string AssayProtocol { get; set; }

        [Required]
        [DisplayName("Assay Length")]
        public decimal AssayLength { get; set; }

    }
}