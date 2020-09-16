using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;
using Plugin.Geolocator;
using Xamarin.Essentials;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace UniDel.Views
{
    public partial class MapPage : ContentPage
    {
        Delivery currentDelivery;

        public MapPage()
        { 
            InitializeComponent();
            locate(128993);
        }
        
        public MapPage(Delivery delivery)
        {
            InitializeComponent();
            locate(delivery.deliveryID);
        }

        private async void locate(int deliveryID)
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);

            var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Deliveries/" + deliveryID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
            Delivery delivery = JsonConvert.DeserializeObject<Delivery>(response);
            CurrentLocation(delivery.deliveryPickupLocation);
        }

        private async void CurrentLocation(String destination)
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);
            String Longitude = position.Longitude.ToString();
            String Latitude = position.Latitude.ToString();
            Longitude.Replace(",", ".");
            Latitude.Replace(",", ".");
            //String destination = "Spar Silver Lakes";
            destination.Replace(" ", "&");
            String currLocation = "https://www.google.com/maps/dir/?api=1&origin="+Latitude+","+Longitude+"&destination=" + destination;
            Console.WriteLine("Location: ", currLocation);
            googleMap.Source = currLocation;
            //currentLocation = loc1;
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
