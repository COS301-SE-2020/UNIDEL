using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace UniDel
{
    // API CALLING for Delivery
    public class Delivery
    {
        public int DeliveryID { get; set; }
        public DateTime DeliveryDate { get; set; }
        public string DeliveryPickupLocation { get; set; }
        public string DeliveryState { get; set; }
        public byte[] DeliveryPicture { get; set; }
        public int DriverID { get; set; }
        //public Driver Driver { get; set; }
        public int VehicleID { get; set; }
        //public Vehicle Vehicle { get; set; }
        public int ClientID { get; set; }
        //public Client Client { get; set; }
        public int CourierCompanyID { get; set; }
        //public CourierCompany CourierCompany { get; set; }

        
    }

}
