using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    class Vehicle
    {
        public int VehicleID { get; set; }

        public string VehicleMake { get; set; }

        public string VehicleModel { get; set; }

        public string VehicleVIN { get; set; }

        public int VehicleMileage { get; set; }

        public string VehicleLicensePlate { get; set; }

        public DateTime VehicleLicenseDiskExpiry { get; set; }

        public DateTime VehicleLastService { get; set; }

        public int VehicleNextMileageService { get; set; }

        public DateTime VehicleNextDateService { get; set; }
    }
}
