using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class MapPage : ContentPage
    {
        private static RestClient client = new RestClient("https://unidelapi.azurewebsites.net/api/");

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
