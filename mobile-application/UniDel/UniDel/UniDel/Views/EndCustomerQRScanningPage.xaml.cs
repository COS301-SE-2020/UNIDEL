using System;
using UniDel.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using UniDel.ViewModels;
//using System.Collections.Generic;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using UniDel.Models;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EndCustomerQRScanningPage : ContentPage
    {
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }
        public Location currentLocation;
        public Location dropOffLocation;
        public bool done = false;
        public bool doubleDone = false;
        public Delivery delivery;
        public Delivery packet;
        public double Kilos;
        //private object indicator;
        private Models.Client client;
        //private HttpClient _client = new HttpClient();


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
                    Delivery(result);

                    // Find the Client's ID
                    //ClientID();
                    //if (client == null)
                    //{
                    //    return;
                    //}

                    //Check if correct Client for package
                    //if (client.ClientName != Session.UserEmail)
                    //{
                    //    DisplayAlert("Invalid package", "This delivery does not belong to you.", "OK");
                    //}
                    //if (done)
                    //{
                        //if (packet == null)
                        //{
                        //    await DisplayAlert("Failed", "The delivery was not found.", "OK");
                        //}
                        //if (packet.deliveryState == "Completed")
                        //{
                        //    await DisplayAlert("Completed", "The delivery has already been completed.", "OK");
                        //}
                        //else
                        //{
                        //    //await ConfirmPackagePostRequest(packet, result);
                        //}
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

        public async Task ConfirmPackagePostRequest(Delivery item, string ID)
        {
            //// if Active change to Confirming
            //if (item.deliveryState == "Active")
            //{
            //    item.deliveryState = "Confirming";
            //}
            //else if (item.deliveryState == "Completed")
            //{
            //    await DisplayAlert("Completed", "Delivery has already been completed.", "OK");
            //    return;
            //}
            //else
            //{
            //    await DisplayAlert("Invalid package state", "Delivery not in correct state.", "OK");
            //    return;
            //}

            ////MAKE POST CALL TO UPDATE DELIVERY DATA
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            //var httpClient = new HttpClient(httpClientHandler);
            //string json = JsonConvert.SerializeObject(item);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            ////indicator.IsRunning = true;
            ////indicator.IsVisible = true;

            ////API POST CALL TO CREATE
            //var response = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/Deliveries/PutDelivery/"+ID, content);
            //if (response.StatusCode == HttpStatusCode.NoContent)
            //{
            //    //await DisplayInfoChangedEventArgs("Confirmation in progress");
            //    await DisplayAlert("Confirming", "Confirmation in progress", "OK");
            //    return;
            //}
            //else
            //{
            //    await DisplayAlert("Confirmation failed", "An error occured while confirming your package", "OK");
            //}

        }

        public async void Delivery(String QR_ID_Scanned)
        {
            try
            {
                var httpClientHandler = new HttpClientHandler();

                httpClientHandler.ServerCertificateCustomValidationCallback =
                (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Deliveries/" + QR_ID_Scanned + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                delivery = JsonConvert.DeserializeObject<Delivery>(response);

                packet = delivery;
                //packet = SearchPacket(delivery, (int)Int64.Parse(QR_ID_Scanned));
                if (packet == null)
                {
                    txtBarcode.Text = "Delivery not found";
                    await DisplayAlert("Delivery not found", "Delivery not found on the system. Try a different QR-Code", "OK");
                    done = false;
                    return;
                }
                if (packet.clientID != Session.ClientID)
                {
                    await DisplayAlert("Invalid delivery owner", "Delivery is meant for someone else. Try a different QR-Code.", "OK");
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
                if (packet.deliveryState == "Active")
                {
                    packet.deliveryState = "Confirming";
                }
                else if (packet.deliveryState == "Confirming")
                {
                    await DisplayAlert("Confirming", "Delivery is already being confirmed. Please let the courier company's driver scan the QR-Code for completing the delivery.", "OK");
                    return;
                }
                else if (packet.deliveryState == "Completed")
                {
                    await DisplayAlert("Completed", "Delivery has already been completed.", "OK");
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
                    await DisplayAlert("Confirming", "Confirmation in progress", "OK");
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

            await Application.Current.MainPage.Navigation.PushModalAsync(new ClientHomePage(), true);
        }
        private Delivery SearchPacket(List<Delivery> d, int email)
        {
            foreach (Delivery u in d)
            {
                if (u.deliveryID == email)
                {
                    return u;
                }
            }
            return null;
        }
    }
}