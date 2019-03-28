using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CompanyD.Models.Requests
{
    public class EmployeeEditRequest
    {
        [Required]
        public int Employee_Id { get; set; }

        [Required(ErrorMessage = "First name is required")]
        [StringLength(50)]
        public string First_Name { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50)]
        public string Last_Name { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        [StringLength(1)]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Employee position is required")]
        [StringLength(128)]
        public string Position { get; set; }

        [Required(ErrorMessage = "Employee salary is required")]
        public decimal Salary { get; set; }

        [Required(ErrorMessage = "Employee department id is required")]
        public int Department_Id { get; set; }
    }
}