﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("OrderStatus")]
    public class OrderStatus
    {
        [Key]
        public int OrderStatusID { get; set; }

        [Required]
        public string OrderStatusDesc { get; set; }

    }
}