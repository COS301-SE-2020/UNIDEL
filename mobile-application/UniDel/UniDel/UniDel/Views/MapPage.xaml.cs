using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;
using Plugin.Geolocator;
using Xamarin.Essentials;
using UniDel.ViewModels;
using Newtonsoft.Json;
using System.Threading.Tasks;
using System.Net.Http;

namespace UniDel.Views
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();
            locate(128993);
        }
        
        public MapPage(int deliveryID)
        {
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("The ID: "+deliveryID);
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            Console.WriteLine("===================");
            InitializeComponent();
            //CurrentLocation();
            locate(deliveryID);
        }

        private async void locate(int deliveryID)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Deliveries/" + deliveryID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                Delivery delivery = JsonConvert.DeserializeObject<Delivery>(response);
                CurrentLocation(delivery.deliveryPickupLocation);
            }
            catch(Exception e)
            {
                Console.WriteLine("Exception: " + e);
                locate(128993);

            }

        }


        private async void CurrentLocation(String destination)
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));
            String Longitude=position.Longitude.ToString();
            String Latitude=position.Latitude.ToString();
            Longitude=Longitude.Replace(",", ".");
            Latitude=Latitude.Replace(",", ".");
            destination=destination.Replace(" ", "&");
            String currLocation = "https://www.google.com/maps/dir/?api=1&origin="+Latitude+","+Longitude+"&destination=" + destination;
            Console.WriteLine("Location: ", currLocation);
            googleMap.Source = currLocation;
            //currentLocation = loc1;
        }

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            indicator.IsVisible = true;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            indicator.IsVisible = false;
            
        }

        void back_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new DriverHomePage();
        }


    }
}
