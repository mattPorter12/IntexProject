using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Material")]
    public class Material
    {

        [Key]
        public int MaterialID { get; set; }

        [Required]
        [Display(Name = "Material Name")]
        public string MatName { get; set; }

        [Required]
        [Display(Name = "Material Cost")]
        public Double MatCost { get; set; }

    }
}