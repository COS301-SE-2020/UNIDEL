using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CourierCompany")]
    public class CourierCompany
    {
        [Column("CourierCompanyID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CourierCompanyID { get; set; }

        [Column("CourierCompanyName")]
        [StringLength(255)]
        public string CourierCompanyName { get; set; }

        [Column("CourierCompanyTelephone")]
        [StringLength(255)]
        public string CourierCompanyTelephone { get; set; }

        [Column("UserID")]
        public int UserID { get; set; }

        [ForeignKey("UserID")]
        public User User { get; set; }
    }
}
