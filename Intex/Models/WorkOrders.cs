using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("WorkOrders")]
    public class WorkOrders
    {
        [Key]
        public int WorkOrderNum { get; set; }

        [DisplayName("Client ID Number")]
        public int ClientID { get; set; }

        [DisplayName("OrderDate")]
        public string OrderDate { get; set; }

        [DisplayName("Order Status")]
        public int OrderStatusID { get; set; }
    }
}