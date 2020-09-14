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
using static Android.Provider.SyncStateContract;
using System.Threading.Tasks;

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
        private HttpClient _client = new HttpClient();


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

                    // Find the Client's ID
                    //ClientID();

                        //Check if correct Client for package


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
                }
            }
            catch (Exception QR_Scanner_ex)
            {
                throw;
                // QR scanned is null
                //throw;
            }
        }

        public async Task SaveTodoItemAsync(Delivery item, string ID)
        {
            string URL = "https://api.unideldeliveries.co.za/api/Deliveries/PutDelivery/"+ID;


            //var post = new Post { Title = "Title " + DateTime.Now.Ticks };
            var content = JsonConvert.SerializeObject(item);
            var response = await _client.PostAsync(URL, new StringContent(content));


        }
    }
}