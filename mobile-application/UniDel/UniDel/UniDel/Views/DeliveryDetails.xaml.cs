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
            {   deliveryID = "Delivery ID : 135467",
                pickupName = "BEX Express SA",
                dropoffName = "SPAR: Silver Lakes",
                dropofLocation = "Cnr Fakkel and, Pretoria St, Silverton, Pretoria, 0001",
                pickupLocation = "309 Derdepoort Rd, Silverton, Pretoria, 0001"
            });
            delivery_details.Add(new DeliveryDetailsViewModel
            {
                deliveryID = "Delivery ID : 135468",
                pickupName = "Dawn Wing",
                dropoffName = "SPAR: Silver Lakes",
                dropofLocation = "Cnr Fakkel and, Pretoria St, Silverton, Pretoria, 0001",
                pickupLocation = "26 Pieter Drive, Republic, Pretoria, 0847"
            });
            delivery_details.Add(new DeliveryDetailsViewModel
            {
                deliveryID = "Delivery ID : 135469",
                pickupName = "Courierit Pty Ltd Pretoria",
                dropoffName = "SPAR: Silver Lakes",
                dropofLocation = "Cnr Fakkel and, Pretoria St, Silverton, Pretoria, 0001",
                pickupLocation = "375 Calliandra St, Montana, Pretoria, 0182"
            });
            delivery_details.Add(new DeliveryDetailsViewModel
            {
                deliveryID = "Delivery ID : 128993",
                pickupName = "Dawn Wing",
                dropoffName = "Pick n Pay: Hatfield",
                dropofLocation = "Hatfield Plaza 1122 Burnett Street &, Grosvenor St, Hatfield, Pretoria, 0083",
                pickupLocation = "26 Pieter Drive, Republic, Pretoria, 0847"
            });
            delivery_details.Add(new DeliveryDetailsViewModel
            {
                deliveryID = "Delivery ID : 128994",
                pickupName = "BEX Express SA",
                dropoffName = "Pick n Pay: Hatfield",
                dropofLocation = "Hatfield Plaza 1122 Burnett Street &, Grosvenor St, Hatfield, Pretoria, 0083",
                pickupLocation = "309 Derdepoort Rd, Silverton, Pretoria, 0001"
            });



        }

        async void Complete_Clicked(object sender, EventArgs e)
        {
            //code here to remove object from list
            bool delivered = await DisplayAlert("Delivery Complete?", "Was the order delivered?", "No", "Yes");
            if (delivered == true)
            {
                await Navigation.PopAsync();
            }
            else
            {
                bool reshedule = await DisplayAlert("Reschedule Delivery?", "Would you like to reschedule delivery?", "Yes", "No");
                if(reshedule== true)
                {
                    await DisplayAlert("Confirmation", "Delivery rescheduled for tomorrow", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await DisplayAlert("Confirmation", "Contact BEX Express SA on : 0123847748", "OK");
                    await Navigation.PopAsync();
                }
            }


        }

        async void btnNavigate_Clicked(Object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }

    }
}
