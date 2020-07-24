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

        void Complete_Clicked(System.Object sender, System.EventArgs e)
        {
            Navigation.PopAsync();
            
        }


    }
}
