using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelAPI.Models
{
    [Table("CompanyVehicle")]
    public class CompanyVehicle
    {
        [Column("CompanyVehicleID")]
        [Key]
        [DatabaseGenerated
            (DatabaseGeneratedOption.Identity)]
        [Required]
        public int CompanyVehicleID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public CourierCompany CourierCompany { get; set; }

        [Column("VehicleID")]
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }
    }
}
