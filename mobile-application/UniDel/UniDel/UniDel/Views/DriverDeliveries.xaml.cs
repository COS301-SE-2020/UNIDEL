using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverDeliveries : ContentPage
    {
        public IList<Delivery> DeliveryDetails { get; set; }

        public DriverDeliveries()
        {
            InitializeComponent();
            DeliveryDetails = new List<Delivery>();
            DeliveryDetails.Add(new Delivery
            {
                deliveryID = "Delivery ID : 135467",
                pickupName = "Pickup : BEX Express SA",
                dropoffName = "Dropoff : SPAR Silver Lakes",
                packageImage = "https://lasership.com/img/section/handle_care.jpg"
            });
            DeliveryDetails.Add(new Delivery
            {
                deliveryID = "Delivery ID : 135467",
                pickupName = "Pickup : BEX Express SA",
                dropoffName = "Dropoff : SPAR Silver Lakes",
                packageImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcSE0LegyE3LvKOU85W9AGyQJYg7DbZ7zw4GQw&usqp=CAU"

            });
            DeliveryDetails.Add(new Delivery
            {
                deliveryID = "Delivery ID : 135467",
                pickupName = "Pickup : BEX Express SA",
                dropoffName = "Dropoff : SPAR Silver Lakes",
                packageImage = "https://encrypted-tbn0.gstatic.com/images?q=tbn%3AANd9GcT96LTGbpdZFmY6ZKVNCpSNOP6BifU-mMZUDw&usqp=CAU"
            });

            BindingContext = this;

        }

        public class Delivery
        {
            public string deliveryID { get; set; }
            public string pickupName { get; set; }
            public string dropoffName { get; set; }
            public string packageImage { get; set; }
        }

        void btnTrack_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MapPage();
        }

        void btnDone_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CompleteDelivery();
        }
    }
}
