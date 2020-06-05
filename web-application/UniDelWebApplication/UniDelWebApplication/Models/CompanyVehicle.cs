﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UniDelWebApplication.Models
{
    [Table("CompanyVehicle")]
    public class CompanyVehicle
    {
        [Column("CourierCompanyID")]
        public int CourierCompanyID { get; set; }

        [ForeignKey("CourierCompanyID")]
        public CourierCompany CourierCompany { get; set; }

        [Column("VehicleID")]
        public int VehicleID { get; set; }

        [ForeignKey("VehicleID")]
        public Vehicle Vehicle { get; set; }
    }
}
