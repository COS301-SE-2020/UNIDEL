﻿using System;
namespace UniDel.ViewModels
{
    public class CurrentDeliveryViewModel
    {
        public int deliveryID { get; set; }
        public string pickupName { get; set; }
        public string dropoffName { get; set; }
        public DateTime deliveryDate { get; set; }
        public string deliveryState { get; set; }
        public string deliveryComment { get; set; }
        public string courierCompany { get; set; }
    }
}
