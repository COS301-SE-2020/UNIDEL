using System;
using Xamarin.Forms;
using UniDel.ViewModels;
using System.Collections.ObjectModel;

namespace UniDel.Views
{
    public partial class DeliveryDetails : ContentPage
    {
        public ObservableCollection<DeliveryDetailsViewModel> delivery_details { get; set; }

        public DeliveryDetails()
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

            delivery_details = new ObservableCollection<DeliveryDetailsViewModel>();

            delivery_details.Add(new DeliveryDetailsViewModel
            { deliveryID = "Delivery ID : 1", pickupName = "The Daily Sun", dropoffName = "Engen", dropofLocation = "178 Belly Avenue, Krooyling, Pretoria, 0083",
              pickupLocation = "26 Pieter Drive, Republic, Pretoria, 0847"});



        }

        async void Complete_Clicked(object sender, EventArgs e)
        {
            //code here to remove object from list
            await DisplayAlert("Confirmation", "Delivery Complete", "OK");
            await Navigation.PopAsync();
        }


    }
}
