using System;
using UniDel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Geolocator;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using UniDel.ViewModels;
//using System.Collections.Generic;
using System.Linq;
using RestSharp.Extensions;
using System.Collections.Generic;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndCustomerQRScanningPage : ContentPage
    {
        public ObservableCollection<CompleteDeliveryViewModel> complete_deliveries { get; set; }
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }
        public Location currentLocation;
        public Location dropOffLocation;
        public bool done = false;
        public bool doubleDone = false;
        public List<Delivery> delivery;
        public Delivery packet;
        public double Kilos;
        private object indicator;
        private Client client;


        public EndCustomerQRScanningPage()
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

                    // API Calls for Scanned QR-Code's ID
                    //Delivery(result);

                    // Find this CourierCompany's ID
                    //CourierCompanyID();

                    // Find the Client for Dropofflocation
                    //ClientID();

                    if (client == null)
                    {
                        return;
                    }
                    if (done == false)
                    {
                        //await DisplayAlert("Completed", "The Delivery has already been completed.", "OK");
                    }
                    else if (packet.deliveryState == "Completed")
                    {
                        await DisplayAlert("Completed", "The Delivery has already been completed.", "OK");
                    }
                    else if (done == true)
                    {
                        // Calculates coordinates of location
                        //ConvertToCoordinates(client.ClientAddress);

                        if (doubleDone == true)
                        {
                            // Calculates kilometers the drop off location of package VS current location of device
                            //LocationDistance(currentLocation, dropOffLocation);

                            if (Kilos <= 30)
                            {
                                // POST REQUEST to change state to Delivered.
                            }
                            else
                            {
                                // Send data to Active Deliveries Page
                                //SetUpDeliveryData(result);
                            }
                        }
                    }





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
    }
}