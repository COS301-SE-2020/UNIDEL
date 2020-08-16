using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace UniDel.Models
{
    class Note
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryPickupLocation { get; set; }
        public string DeliveryState { get; set; }
        public string DeliveryPicture { get; set; }
        public string DeliveryID { get; set; }
        public string VehicleID { get; set; }
        public string ClientID { get; set; } // Perhaps becomes Client Name
        public string CourierCompanyID { get; set; } // Perhaps becomes Courier Company Name
    }
}
