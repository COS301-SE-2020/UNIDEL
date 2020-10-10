using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UniDelAPI.Models
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

        [Column("DriverID")]
        public int DriverID { get; set; }

        [ForeignKey("DriverID")]
        public Driver Driver { get; set; }

        [Column("VehicleID")]
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }
    }
}
