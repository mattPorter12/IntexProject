using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Intex.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Key]
        [Required]
        public int EmployeeId { get; set; }

        [Required]
        [DisplayName("Employee First Name")]
        public string EmpFirstName { get; set; }

        [Required]
        [DisplayName("Employee Last Name")]
        public string EmpLastName { get; set; }

        [Required]
        [DisplayName("Employee Wage")]
        public double HourlyWage { get; set; }
    }
}