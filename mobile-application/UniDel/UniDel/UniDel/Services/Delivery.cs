using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using UniDel.Data;

namespace UniDel
{
    // API CALLING for Delivery
    public class Delivery
    {
        public int deliveryID { get; set; }
        public DateTime deliveryDate { get; set; }
        public string deliveryPickupLocation { get; set; }
        public string deliveryState { get; set; }
        public byte[] deliveryPicture { get; set; }
        public int driverID { get; set; }
        public string Driver { get; set; }
        public int vehicleID { get; set; }
        public string Vehicle { get; set; }
        public int clientID { get; set; }
        public string client { get; set; }
        public int courierCompanyID { get; set; }
        public string CourierCompany { get; set; }
        public string comment { get; set; }

    }

}
