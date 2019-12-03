using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("TestMaterial")]
    public class TestMaterial
    {

        [Key]
        public int TestID { get; set; }

        [Key]
        public int MaterialId { get; set; }
    }
}