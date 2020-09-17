using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("Delivery")]
    public class Delivery
    {
        [Column("DeliveryID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int DeliveryID { get; set; }

        [Column("DeliveryDate")]
        public DateTime DeliveryDate { get; set; }

        [Column("DeliveryPickupLocation")]
        [StringLength(255)]
        public string DeliveryPickupLocation { get; set; }

        [Column("DeliveryState")]
        [StringLength(255)]
        public string DeliveryState { get; set; }

        [Column("DeliveryPicture")]
        public byte[] DeliveryPicture { get; set; }

        [Column("DriverID")]
        public int DriverID { get; set; }

        [ForeignKey("DriverID")]
        public Driver Driver { get; set; }

        [Column("ClientID")]
        public int ClientID { get; set; }
    }
}