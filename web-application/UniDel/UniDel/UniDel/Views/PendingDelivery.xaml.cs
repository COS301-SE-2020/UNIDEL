using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class PendingDelivery : ContentPage
    {
        public PendingDelivery()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();

        }

        void Details_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PushAsync(new DeliveryDetails());
        }

        //void Pickup_Clicked(System.Object sender, System.EventArgs e)
        //{
        //    Navigation.PushAsync(new ActiveDelivery());

        //}

        
        private async void Pickup_Clicked(object sender, EventArgs e)
        {
            await App.Nav.PushAsync(new ActiveDelivery());
                      
        }

    }
}
