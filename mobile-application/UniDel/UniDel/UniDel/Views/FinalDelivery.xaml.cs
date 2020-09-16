using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class FinalDelivery : ContentPage
    {
        public FinalDelivery()
        {
            InitializeComponent();
        }

        void btnScan_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new QRScanningPage();

        }

    }
}
