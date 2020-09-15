using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CompanyDriver")]
    public class CompanyDriver
    {
        [Column("CompanyDriverID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CompanyDeliveryID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public int CourierCompanyID { get; set; }

        [Column("DriverID")]
        public int DriverID { get; set; }
    }
}
