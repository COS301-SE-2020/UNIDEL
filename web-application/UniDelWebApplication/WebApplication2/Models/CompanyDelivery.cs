using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CompanyDelivery")]
    public class CompanyDelivery
    {
        [Column("CompanyDeliveryID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CompanyDeliveryID { get; set; }

        [Column("CourierCompanyID")]
        public int CourierCompanyID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public CourierCompany CourierCompany { get; set; }

        [Column("DeliveryID")]
        public int DeliveryID { get; set; }

        [ForeignKey("DeliveryID")]
        public Delivery Delivery { get; set; }
    }
}
