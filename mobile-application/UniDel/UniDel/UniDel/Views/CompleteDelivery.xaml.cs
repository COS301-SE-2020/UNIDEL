using System;
using System.Collections.ObjectModel;
using UniDel.ViewModels;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class CompleteDelivery : ContentPage
    {
        public ObservableCollection<CompleteDeliveryViewModel> active_deliveries { get; set; }

        public CompleteDelivery()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();

            BindingContext = this;

            //apidata = [{ }]
            //foreach (x) {
            //    complete_deliveries.Add(new CompleteDeliveryViewModel
            //    { deliveryID = "Delivery ID: "+ X.id,
            //        pickupName = "Pickup : " + X.pickupname,
            //        dropoffName = "Drop Off : " + X.dropoffname });

            //}

            active_deliveries = new ObservableCollection<CompleteDeliveryViewModel>();

            active_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "Delivery ID : 128993", pickupName = "Dawn Wing", dropoffName = "Pick n Pay: Hatfield" });
            active_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "Delivery ID : 128994", pickupName = "BEX Express SA", dropoffName = "Pick n Pay: Hatfield" });

            completeView.ItemsSource = active_deliveries;
        }
    }
}
