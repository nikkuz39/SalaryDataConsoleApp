using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryDataConsoleApp.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [Column(TypeName = "varchar(100)")]
        public string Name { get; set; }
        public int Salary { get; set; }
        public int? Chief_Id { get; set; }

        public int DepartmentId { get; set; }
        public Department Department { get; set; }
    }
}
