using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("Vehicle")]
    public class Vehicle
    {
        [Column("VehicleID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int VehicleID { get; set; }

        [Column("VehicleMake")]
        [StringLength(255)]
        public string VehicleMake { get; set; }

        [Column("VehicleModel")]
        [StringLength(255)]
        public string VehicleModel { get; set; }

        [Column("VehicleVIN")]
        [StringLength(255)]
        public string VehicleVIN { get; set; }

        [Column("VehicleMileage")]
        public int VehicleMileage { get; set; }

        [Column("VehicleLicensePlate")]
        [StringLength(255)]
        public string VehicleLicensePlate { get; set; }

        [Column("VehicleLicenseDiskExpiry")]
        public DateTime VehicleLicenseDiskExpiry { get; set; }

        [Column("VehicleLastService")]
        public DateTime VehicleLastService { get; set; }

        [Column("VehicleNextMileageService")]
        public int VehicleNextMileageService { get; set; }

        [Column("VehicleNextDateService")]
        public DateTime VehicleNextDateService { get; set; }
    }
}
