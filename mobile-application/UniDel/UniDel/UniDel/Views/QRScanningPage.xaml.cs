using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UniDel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanningPage : ContentPage
    {
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

                    try
                    {
                        Console.WriteLine("Trying to get current location...");
                        var requestLocation = new GeolocationRequest(GeolocationAccuracy.Medium);

                        Console.WriteLine("Checking last known location...");
                        var location = await Geolocation.GetLastKnownLocationAsync(); 

                        if (location == null)
                        {
                            Console.WriteLine("Requesting current location via API...");
                            location = await Geolocation.GetLocationAsync(requestLocation);
                        }

                        if (location != null)
                        {
                            Console.WriteLine("Current location found...");
                            Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");

                            // Calculate Distance between two locations
                                Location boston = new Location(42.358056, -71.063611);
                                Location sanFrancisco = new Location(37.783333, -122.416667);

                                double miles = Location.CalculateDistance(boston, sanFrancisco, DistanceUnits.Kilometers);
                            
                        }
                        else
                        {
                            Console.WriteLine("Failed to obtain current location... ");
                        }
                    }
                    catch (FeatureNotSupportedException fnsEx)
                    {
                        throw;
                    }
                    catch (FeatureNotEnabledException fneEx)
                    {
                        throw;
                    }
                    catch (PermissionException pEx)
                    {
                        throw;
                    }
                    catch (Exception QR_Location_ex)
                    {
                        // Unable to get location
                        throw;
                    }
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