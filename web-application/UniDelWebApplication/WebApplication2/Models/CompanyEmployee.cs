using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CompanyEmployee")]
    public class CompanyEmployee
    {
        [Column("CompanyEmployeeID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CompanyEmployeeID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public int CourierCompanyID { get; set; }

        [Column("EmployeeID")]
        public int EmployeeID { get; set; }
    }
}