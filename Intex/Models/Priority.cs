using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Priority")]
    public class Priority
    {

        [Key]
        [Required]
        public int PriorityNumber { get; set; }

        [Required]
        public string PriorityDesc { get; set; }
    }
}