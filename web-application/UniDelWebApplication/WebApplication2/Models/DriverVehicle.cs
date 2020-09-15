using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("DriverVehicle")]
    public class DriverVehicle
    {
        [Column("DriverVehicleID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int DriverVehicleID { get; set; }

        [ForeignKey("DriverID")]
        public int DriverID { get; set; }

        [Column("VehicleID")]
        public int VehicleID { get; set; }
    }
}