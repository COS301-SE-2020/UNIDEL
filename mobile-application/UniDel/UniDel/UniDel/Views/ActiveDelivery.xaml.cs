using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class ActiveDelivery : ContentPage
    {
        public ActiveDelivery()
        {
            InitializeComponent();
        }

        private async void Navigate_Clicked(object sender, EventArgs e)
        {
            await App.Nav.PushAsync(new MapPage());
        }

        void Complete_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
        }


    }
}
