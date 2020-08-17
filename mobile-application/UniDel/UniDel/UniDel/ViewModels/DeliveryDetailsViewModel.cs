using System;
namespace UniDel.ViewModels
{
    public class DeliveryDetailsViewModel
    {
        public string deliveryID { get; set; }
        public string pickupName { get; set; }
        public string dropoffName { get; set; }
        public string pickupLocation { get; set; }
        public string dropofLocation { get; set; }
        public string packageImage { get; set; }
    }
}