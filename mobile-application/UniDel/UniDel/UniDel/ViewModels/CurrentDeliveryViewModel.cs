using System;
namespace UniDel.ViewModels
{
    public class CurrentDeliveryViewModel
    {
        public string deliveryID { get; set; }
        public string pickupName { get; set; }
        public string dropoffName { get; set; }
        public DateTime deliveryDate { get; set; }
    }
}
