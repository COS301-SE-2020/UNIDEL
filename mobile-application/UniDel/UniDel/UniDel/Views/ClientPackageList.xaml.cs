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
using UniDel.Models;

namespace UniDel.Views
{
    public partial class ClientPackageList : ContentPage
    {
        public ClientPackageList()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();
            BindingContext = this;
            ListingAPI();
        }

        public List<CurrentDeliveryViewModel> active_deliveries { get; set; }


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
            //bool testy;
           

            foreach (var item in delivery_data)
            {
               /*testy = (item.clientID == Session.ClientID);
                active_deliveries.Add(new CurrentDeliveryViewModel()
                {
                    deliveryState = Session.ClientID.ToString(),
                    courierCompany = item.clientID.ToString(),
                    pickupName = testy.ToString()
                } );*/
                if ((item.clientID == Session.ClientID)&&(item.deliveryState.ToLower()!= "completed"))
                {
                    /*nt id = item.clientID;
                    Client client_data = null;
                    var response2 = await httpClient.GetStringAsync(Constants.BaseURL + "Clients/" + id + "?" + Constants.Token);
                    client_data = JsonConvert.DeserializeObject<Client>(response2);*/

                    active_deliveries.Add(new CurrentDeliveryViewModel()
                    {
                        deliveryDate=item.deliveryDate,
                        deliveryState=item.deliveryState,
                        courierCompany=item.CourierCompany,
                        pickupName=item.deliveryPickupLocation,
                        /*deliveryState = item.deliveryState.ToUpper(),
                        pickupName = item.deliveryPickupLocation,
                        dropoffName = client_data.ClientAddress,
                        deliveryID = item.deliveryID,*/
                    });
                }
            }

           indicator.IsRunning = false;
            indicator.IsVisible = false;

            activeView.ItemsSource = active_deliveries;
        }

        void btnTrack_Clicked(System.Object sender, System.EventArgs e)
        {
            //Application.Current.MainPage = new MapPage(int.Parse(activeView.ClassId));
            try
            {
                Application.Current.MainPage = new MapPage(int.Parse(((Button)sender).ClassId));
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex);
                Application.Current.MainPage = new MapPage(128993);
            }
        }
    }
}
