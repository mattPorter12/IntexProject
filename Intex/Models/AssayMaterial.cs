using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("AssayMaterial")]
    public class AssayMaterial
    {

        
        public int AssayID { get; set; }

        
        public int MaterialID { get; set; }

    }
}