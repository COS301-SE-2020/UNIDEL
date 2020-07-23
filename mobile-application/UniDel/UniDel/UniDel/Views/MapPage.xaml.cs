using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class MapPage : ContentPage
    {

        public MapPage()
        {
            InitializeComponent();
        }

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
        }
    }
}
