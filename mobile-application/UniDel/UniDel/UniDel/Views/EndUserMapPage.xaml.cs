using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;
using Plugin.Geolocator;
using Xamarin.Essentials;

namespace UniDel.Views
{
    public partial class EndUserMapPage : ContentPage
    {

        public EndUserMapPage()
        {
            InitializeComponent();
            CurrentLocation();
        }

        private async void CurrentLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);
            String destination = "Spar Silver Lakes";
            destination.Replace(" ", "&");
            String currLocation = "https://www.google.com/maps/search/?api=1&parameters&query=" + position.Latitude+","+position.Longitude;
            Console.WriteLine("Location: ", currLocation);
            googleMap.Source = currLocation;
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
