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
using System.Text;
using System.Net;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QRScanningPage : ContentPage
    {
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }

        public Location currentLocation;
        public Location dropOffLocation;
        public bool done = false;
        public bool doubleDone = false;
        public Delivery delivery;
        public Delivery packet;
        public double Kilos;
        private object indicator;
        private Client client;

        public QRScanningPage()
        {
            InitializeComponent();
            // Get device's current location
            CurrentLocation();
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

                    // Make the API calls here.
                    DriverAPI(result);

                    // API Calls for Scanned QR-Code's ID
                    //Delivery(result);

                    // Find this CourierCompany's ID: https://api.unideldeliveries.co.za/api/CourierCompanies
                    //CourierCompanyID();

                    // Find the Client for Dropofflocation
                    //ClientID();

                    if (client == null)
                    {
                        return;
                    }
                    if(done == false)
                    {
                        //await DisplayAlert("Completed", "The Delivery has already been completed.", "OK");
                    }
                    else if(packet.deliveryState == "Completed")
                    {
                        await DisplayAlert("Completed", "The Delivery has already been completed.", "OK");
                    }
                    else if (done == true)
                    {
                        // Calculates coordinates of location
                        ConvertToCoordinates(client.ClientAddress);

                        if (doubleDone == true)
                        {
                            // Calculates kilometers the drop off location of package VS current location of device
                            LocationDistance(currentLocation, dropOffLocation);

                            if (Kilos <= 30)
                            {
                                // POST REQUEST to change state to Delivered.
                            }
                            else
                            {
                                // Send data to Active Deliveries Page
                                SetUpDeliveryData(Convert.ToInt32(result));
                            }
                        }
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

        private async void DriverAPI(string QR_ID_Scanned)
        {
            try
            {

                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Deliveries/" + QR_ID_Scanned + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                packet = JsonConvert.DeserializeObject<Delivery>(response);

                if (packet == null)
                {
                    txtBarcode.Text = "Delivery not found";
                    await DisplayAlert("Delivery not found", "Delivery not found on the system. Try a different QR-Code", "OK");
                    done = false;
                    return;
                }

                if (packet.deliveryState == "Completed")
                {
                    await DisplayAlert("Completed", "The delivery has already been completed.", "OK");
                    return;
                }

                Console.WriteLine("Delivery State: " + packet.deliveryState);


                Console.WriteLine(response);
                Console.WriteLine("....DeliveryID: " + packet.deliveryID + " CourierCompany: " + packet.CourierCompany + " PickupLocation: " + packet.deliveryPickupLocation);

                done = true;

                // if Active change to Confirming
                if (packet.deliveryState == "Pending")
                {
                    packet.deliveryState = "Active";
                }
                else if (packet.deliveryState == "Confirming")
                {
                    packet.deliveryState = "Completed";
                }
                else if (packet.deliveryState == "Active")
                {
                    await DisplayAlert("Delivery already active", "Delivery is already being delivered. Check the active page for more details.", "OK");
                    return;
                }
                else if (packet.deliveryState == "Completed")
                {
                    await DisplayAlert("Completed", "Delivery has already been completed.", "OK");
                    return;
                }
                else if (packet.deliveryState == "Placed")
                {
                    await DisplayAlert("Delivery not assigned", "Please contact your courier company for the correct package.", "OK");
                    return;
                }
                else
                {
                    await DisplayAlert("Invalid package state", "Delivery not in correct state.", "OK");
                    return;
                }

                //MAKE POST CALL TO UPDATE DELIVERY DATA
                httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                httpClient = new HttpClient(httpClientHandler);
                string json = JsonConvert.SerializeObject(packet);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                //indicator.IsRunning = true;
                //indicator.IsVisible = true;

                //API POST CALL TO CREATE
                var response2 = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/Deliveries/PutDelivery/" + QR_ID_Scanned + "?k=UDL2Avv378jBBgd772hFSbbsfwUD", content);
                if (response2.StatusCode == HttpStatusCode.NoContent)
                {
                    //await DisplayInfoChangedEventArgs("Confirmation in progress");
                    //await DisplayAlert("Confirming", "Confirmation in progress", "OK");
                    return;
                }
                else
                {
                    await DisplayAlert("Confirmation failed", "An error occured while confirming your package", "OK");
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("QR-Scanning Error", e.Message, "OK");
            }
        }

        public void SetUpDeliveryData(int id)
        {
            active_deliveries = new ObservableCollection<CurrentDeliveryViewModel>();
            active_deliveries.Add(new CurrentDeliveryViewModel
            {
                deliveryID = id,
                // clientname = client.ClientName,
                pickupName = packet.deliveryPickupLocation,
                dropoffName = client.ClientAddress
            });
        }

        //public async void Delivery(String QR_ID_Scanned)
        //{
        //    var httpClientHandler = new HttpClientHandler();

        //    httpClientHandler.ServerCertificateCustomValidationCallback =
        //    (message, cert, chain, errors) => { return true; };

        //    var httpClient = new HttpClient(httpClientHandler);

        //    //var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
        //    //var httpClient = new HttpClient();
        //    var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Deliveries/" + QR_ID_Scanned);
        //    //var delivery = JsonConvert.DeserializeObject<Delivery>(response);
        //    delivery = JsonConvert.DeserializeObject<Delivery>(response);


        //    packet = delivery;
        //    //packet = SearchPacket(delivery, (int)Int64.Parse(QR_ID_Scanned));
        //    if (packet == null)
        //    {
        //        txtBarcode.Text = "Delivery not found";
        //        await DisplayAlert("Delivery not found", "Delivery not found on the system. Try a different QR-Code", "OK");
        //        done = false;
        //        return;
        //    }

        //    Console.WriteLine("Delivery State: " + packet.deliveryState);


        //    Console.WriteLine(response);
        //    Console.WriteLine("....DeliveryID: " + packet.deliveryID + " CourierCompany: " + packet.CourierCompany + " PickupLocation: " + packet.deliveryPickupLocation);

        //    done = true;
        //}

        private void LocationDistance(Location loc1, Location loc2)
        {
            Kilos = Location.CalculateDistance(loc1, loc2, DistanceUnits.Kilometers);
            Console.WriteLine("...Distance between " + loc1 + " and " + loc2 + " is " + Kilos + "kms....");
        }

        private async void CurrentLocation()
        {
            var locator = CrossGeolocator.Current;

            var position = await locator.GetPositionAsync(TimeSpan.FromSeconds(10));

            Console.WriteLine("Position Status: {0}", position.Timestamp);
            Console.WriteLine("Position Latitude: {0}", position.Latitude);
            Console.WriteLine("Position Longitude: {0}", position.Longitude);

            Location loc1 = new Location(position.Latitude, position.Longitude);
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
                    doubleDone = true;
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

        //private Delivery SearchPacket(List<Delivery> d, int email)
        //{
        //    foreach (Delivery u in d)
        //    {
        //        if (u.deliveryID == email)
        //        {
        //            return u;
        //        }
        //    }
        //    return null;
        //}

        private Client SearchClient(List<Client> d, int c)
        {
            foreach (Client u in d)
            {
                if (u.ClientID == c)
                {
                    return u;
                }
            }
            return null;
        }

        private async void CourierCompanyID()
        {
            //var httpClientHandler = new HttpClientHandler();

            //httpClientHandler.ServerCertificateCustomValidationCallback =
            //(message, cert, chain, errors) => { return true; };

            //var httpClient = new HttpClient(httpClientHandler);

            ////var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            ////var httpClient = new HttpClient();
            //var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/clients");
            ////var delivery = JsonConvert.DeserializeObject<Delivery>(response);
            //delivery = JsonConvert.DeserializeObject<List<Delivery>>(response);

            //courier = SearchPacket(delivery, (int)Int64.Parse(QR_ID_Scanned));
            //if (courier == null)
            //{
            //    txtBarcode.Text = "Delivery not found";
            //    await DisplayAlert("Delivery not found", "Delivery not found on the system. Try a different QR-Code", "OK");
            //    done = false;
            //    return;
            //}

            //Console.WriteLine("Delivery State: " + packet.deliveryState);


            //Console.WriteLine(response);
            //Console.WriteLine(packet);

            //done = true;
        }

        private async void ClientID()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);

            //var httpClient = new HttpClient(new System.Net.Http.HttpClientHandler());
            //var httpClient = new HttpClient();
            var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/clients");
            //var delivery = JsonConvert.DeserializeObject<Delivery>(response);
            var clients = JsonConvert.DeserializeObject<List<Client>>(response);

            client = SearchClient(clients, packet.clientID);
            if (client == null)
            {
                txtBarcode.Text = "Client not found";
                await DisplayAlert("Client not found", "Client not found on the system. Try a different QR-Code", "OK");
                done = false;
                return;
            }

            Console.WriteLine(response);
            Console.WriteLine("...ClientID: "+client.ClientID+ " ClientAddress: " +client.ClientAddress + " ClientName: " + client.ClientName);
        }



        void back_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new DriverHomePage();
        }


    }


}