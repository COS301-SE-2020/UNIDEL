using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UniDel.Data;
using UniDel.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;
using System.Text;
using UniDel.Models;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverDeliveries : ContentPage
    {
        public IList<PendingDelivery> DeliveryDetails { get; set; }

        public DriverDeliveries()
        {

            InitializeComponent();
            BindingContext = this;
            DisplayList();

        }

        public async void DisplayList()
        {
            IList<Delivery> data = null;
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(httpClientHandler);

            indicator.IsRunning = true;
            indicator.IsVisible = true;

            await Task.Run(async () =>
            {
                var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Deliveries?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                data = JsonConvert.DeserializeObject<IList<Delivery>>(response);
            });

            if (data == null)
            {
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                await DisplayAlert("Empty", "No Pending Deliveries", "OK");
                return;
            }

            foreach (var item in data)
            {
                if(item.deliveryState == "pending")
                {
                    DeliveryDetails.Add(new PendingDelivery()
                    {
                        deliveryID = item.deliveryID,
                        pickupName = item.deliveryPickupLocation,
                        dropoffName = item.client
                    });
                }
            }

            indicator.IsRunning = false;
            indicator.IsVisible = false;

        }

        public class PendingDelivery
        {
            public int deliveryID { get; set; }
            public string pickupName { get; set; }
            public string dropoffName { get; set; }
        }

        void btnTrack_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MapPage();
        }

        void btnDone_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CompleteDelivery();
        }
    }
}
