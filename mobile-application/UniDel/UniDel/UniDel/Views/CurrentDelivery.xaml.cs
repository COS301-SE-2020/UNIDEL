﻿using System;
using Xamarin.Forms;
using UniDel.ViewModels;
using System.Collections.ObjectModel;

namespace UniDel.Views
{
    public partial class CurrentDelivery : ContentPage
    {
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }

        public CurrentDelivery()
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

            active_deliveries = new ObservableCollection<CurrentDeliveryViewModel>();

            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 1", pickupName = "BLEH2", dropoffName = "BLEH" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 2", pickupName = "BLEH2", dropoffName = "BLEH" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 3", pickupName = "BLEH2", dropoffName = "BLEH" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 4", pickupName = "BLEH2", dropoffName = "BLEH" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 5", pickupName = "BLEH2", dropoffName = "BLEH" });


            activeView.ItemsSource = active_deliveries;

        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRScanningPage());
        }

        async void Collect_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeliveryDetails());
        }

    }
}