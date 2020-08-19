using System;
using UniDel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Android.Database;
using System.Collections.ObjectModel;
using UniDel.ViewModels;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanningPage : ContentPage
    {
        public ObservableCollection<CompleteDeliveryViewModel> complete_deliveries { get; set; }
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }

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

            
            AfterQRScan();
            //Delivery();
            LocationDistance();
            SetUpDeliveryData();
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

        public async void Delivery()
        {
            

            var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://localhost:44362/api/Deliveries");
            var delivery = JsonConvert.DeserializeObject<List<Delivery>>(response);

            Console.WriteLine(delivery);
        }

        private void LocationDistance()
        {
            Location loc1 = new Location(-26.119151, 27.741674);
            

            Location boston = new Location(42.358056, -71.063611);
            Location sanFrancisco = new Location(37.783333, -122.416667);
            double Kilos = Location.CalculateDistance(boston, sanFrancisco, DistanceUnits.Kilometers);
            Console.WriteLine("...Distance between " + boston + " and " + sanFrancisco + " is " + Kilos+"kms....");
        }

        private async void AfterQRScan()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);

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



    }
}