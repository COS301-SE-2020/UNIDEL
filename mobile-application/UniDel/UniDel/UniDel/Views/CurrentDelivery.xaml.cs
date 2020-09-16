using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniDel.Data;
using UniDel.Services;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentDelivery : ContentPage
    {
        private string location;

        public List<CurrentDeliveryViewModel> active_deliveries { get; set; }

        public CurrentDelivery()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();
            BindingContext = this;
            ListingAPI();
            //apidata = [{ }]
            //foreach (x)
            //{
            //    complete_deliveries.Add(new CompleteDeliveryViewModel
            //    {
            //        deliveryID = "Delivery ID: " + X.id,
            //        pickupName = "Pickup : " + X.pickupname,
            //        dropoffName = "Drop Off : " + X.dropoffname
            //    });

            //}

            //active_deliveries = new List<CurrentDeliveryViewModel>();

            //active_deliveries.Add(new CurrentDeliveryViewModel
            //{ deliveryID = "Delivery ID : 135467", pickupName = "BEX Express SA", dropoffName = "SPAR: Silver Lakes" });
            //active_deliveries.Add(new CurrentDeliveryViewModel
            //{ deliveryID = "Delivery ID : 135468", pickupName = "Dawn Wing ", dropoffName = "SPAR: Silver Lakes" });
            //active_deliveries.Add(new CurrentDeliveryViewModel
            //{ deliveryID = "Delivery ID : 135469", pickupName = "Courierit Pty Ltd Pretoria", dropoffName = "SPAR: Silver Lakes" });


            //activeView.ItemsSource = active_deliveries;

        }

        public async void ListingAPI()
        {
            List<Delivery> delivery_data = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(httpClientHandler);

            indicator.IsRunning = true;
            indicator.IsVisible = true;

            var response = await httpClient.GetStringAsync(Constants.BaseURL + "Deliveries/GetAllDeliveries?" + Constants.Token);
            delivery_data = JsonConvert.DeserializeObject<List<Delivery>>(response);
            active_deliveries = new List<CurrentDeliveryViewModel>();
            foreach (var item in delivery_data)
            {
                if (item.deliveryState.ToLower() == "pending")
                {
                    //int id = item.clientID;
                    //List<Client> client_data = null;
                    //var response2 = await httpClient.GetStringAsync(Constants.BaseURL + "Clients/" + id +"?" + Constants.Token);
                    //client_data = JsonConvert.DeserializeObject<List<Client>>(response2);

                    //location = client_data[0].ClientAddress;
                    active_deliveries.Add(new CurrentDeliveryViewModel()
                    {
                        deliveryState = item.deliveryState.ToUpper(),
                        pickupName = item.deliveryPickupLocation,
                        dropoffName = item.client
                    }); 
                }
            }

            indicator.IsRunning = false;
            indicator.IsVisible = false;

            activeView.ItemsSource = active_deliveries;
        }

        void btnTrack_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MapPage();
        }

        void btnDeliver_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new FinalDelivery();
        }

    }
}
