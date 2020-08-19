﻿using System;
using UniDel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json;
//using System.Collections.Generic;
using Android.Database;
using System.Collections.ObjectModel;
using UniDel.ViewModels;
using System.Collections.Generic;
using System.Linq;
using RestSharp.Extensions;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanningPage : ContentPage
    {
        public ObservableCollection<CompleteDeliveryViewModel> complete_deliveries { get; set; }
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }
        public Location currentLocation;
        public Location dropOffLocation;

        public QRScanningPage()
        {
            InitializeComponent();
        }

        private async void btnScan_Clicked(object sender, EventArgs e)
        {
            try
            {
                var scanner = DependencyService.Get<IQrScanningService>();
                // result of the QR Scan aka unique ID
                    var result = await scanner.ScanAsync();
                if (result != null)
                {
                    txtBarcode.Text = result;

                    Console.WriteLine("QR Scanned");

                    // Get device's current location
                    CurrentLocation();

                    // API Calls for Scanned QR-Code's ID
                    Delivery(result);

                    // Calculates coordinates of location
                    ConvertToCoordinates("Silver Lakes SA");

                    // Calculates kilometers the drop off location of package VS current location of device
                    LocationDistance(currentLocation, dropOffLocation);

                    // Send data to Active Deliveries Page
                    SetUpDeliveryData();

                    
                    

                    //try
                    //{
                    //    Console.WriteLine("Trying to get current location...");
                    //    var requestLocation = new GeolocationRequest(GeolocationAccuracy.Medium);

                    //    Console.WriteLine("Checking last known location...");
                    //    var location = await Geolocation.GetLastKnownLocationAsync(); 

                    //    if (location == null)
                    //    {
                    //        Console.WriteLine("Requesting current location via API...");
                    //        location = await Geolocation.GetLocationAsync(requestLocation);
                    //    }

                    //    if (location != null)
                    //    {
                    //        Console.WriteLine("Current location found...");
                    //        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                    //        // Calculate Distance between two locations
                    //            Location boston = new Location(42.358056, -71.063611);
                    //            Location sanFrancisco = new Location(37.783333, -122.416667);

                    //            double miles = Location.CalculateDistance(boston, sanFrancisco, DistanceUnits.Kilometers);

                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Failed to obtain current location... ");
                    //    }
                    //}
                    //catch (FeatureNotSupportedException fnsEx)
                    //{
                    //    throw;
                    //}
                    //catch (FeatureNotEnabledException fneEx)
                    //{
                    //    throw;
                    //}
                    //catch (PermissionException pEx)
                    //{
                    //    throw;
                    //}
                    //catch (Exception QR_Location_ex)
                    //{
                    //    // Unable to get location
                    //    throw;
                    //}
                }
            }
            catch (Exception QR_Scanner_ex)
            {
                throw;
                // QR scanned is null
                //throw;
            }


           
        }

        public void SetUpDeliveryData()
        {
            active_deliveries = new ObservableCollection<CurrentDeliveryViewModel>();
            active_deliveries.Add(new CurrentDeliveryViewModel
            {
                deliveryID = "JHASDY12",
                pickupName = "Dawn Wing",
                dropoffName = "SPAR: Silver Lakes"
            });

            active_deliveries.Add(new CurrentDeliveryViewModel
            {
                deliveryID = "IASO28G2",
                pickupName = "BEX Express",
                dropoffName = "Macro Centurion"
            });

            //DeliveriesData._array[DeliveryData.DeliveryID] = "JHASDY12";

        }

        public async void Delivery(String QR_ID_Scanned)
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);

            //var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            //var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Deliveries");
            //var delivery = JsonConvert.DeserializeObject<List<Delivery>>(response);

            Console.WriteLine(response);
        }

        private void LocationDistance(Location loc1, Location loc2)
        {
            double Kilos = Location.CalculateDistance(loc1, loc2, DistanceUnits.Kilometers);
            Console.WriteLine("...Distance between " + loc1 + " and " + loc2 + " is " + Kilos + "kms....");
        }

        private async void CurrentLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);

            Location loc1 = new Location(-26.119151, 27.741674);
            currentLocation = loc1;

            //try
            //{
            //    Console.WriteLine("loooking for location...... !");
            //    var request = new GeolocationRequest(GeolocationAccuracy.Medium);
            //    var location = await Geolocation.GetLocationAsync(request);

            //    Console.WriteLine("after awaaaaait...... !");
            //    if (location != null)
            //    {
            //        Console.WriteLine("Location found !");
            //        Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
            //    }
            //}
            //catch (FeatureNotSupportedException fnsEx)
            //{
            //    // Handle not supported on device exception
            //}
            //catch (FeatureNotEnabledException fneEx)
            //{
            //    // Handle not enabled on device exception
            //}
            //catch (PermissionException pEx)
            //{
            //    // Handle permission exception
            //}
            //catch (Exception ex)
            //{
            //    // Unable to get location
            //}
        }

        private async void ConvertToCoordinates(String address)
        {
            try
            {
                //address = "Microsoft Building 25 Redmond WA USA";
                var locations = await Geocoding.GetLocationsAsync(address);

                var location = locations?.FirstOrDefault();
                if (location != null)
                {
                    dropOffLocation = location;
                    Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                }

            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature not supported on device
            }
            catch (Exception ex)
            {
                // Handle exception that may have occurred in geocoding
            }
        }


    }
}