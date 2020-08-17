using System;
using System.Collections.ObjectModel;
using UniDel.ViewModels;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class CompleteDelivery : ContentPage
    {
        public ObservableCollection<CompleteDeliveryViewModel> complete_deliveries { get; set; }

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

            complete_deliveries = new ObservableCollection<CompleteDeliveryViewModel>();

            complete_deliveries.Add(new CompleteDeliveryViewModel
                { deliveryID = "1", pickupName = "BLEH2", dropoffName = "BLEH" });
            complete_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "2", pickupName = "BLEH2", dropoffName = "BLEH" });
            complete_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "3", pickupName = "BLEH2", dropoffName = "BLEH" });
            complete_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "4", pickupName = "BLEH2", dropoffName = "BLEH" });
            complete_deliveries.Add(new CompleteDeliveryViewModel
            { deliveryID = "5", pickupName = "BLEH2", dropoffName = "BLEH" });

            completeView.ItemsSource = complete_deliveries;
        }
    }
}
