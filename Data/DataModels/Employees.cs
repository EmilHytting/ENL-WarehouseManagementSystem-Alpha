using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENL___WarehouseManagementSystem.Data.DataModels
{
    [Table("Employees")]

   
    public class Employee
    {
        [Key]

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public string EmpEmail { get; set; }
        public string EmpPhone { get; set; }
        public DateTime CreatedDate { get; set; }
        public string EmploymentStatus { get; set; }
    }
}
