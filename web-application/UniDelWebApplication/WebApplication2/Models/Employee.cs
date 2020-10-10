using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("Employee")]
    public class Employee
    {
        [Column("EmployeeID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int EmployeeID { get; set; }

        [Column("EmployeeName")]
        [StringLength(255)]
        public string EmployeeName { get; set; }

        [Column("EmployeeCellphone")]
        [StringLength(20)]
        public string EmployeeCellphone { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [Column("UserType")]
        [StringLength(255)]
        public string UserType { get; set; }
    }
}
