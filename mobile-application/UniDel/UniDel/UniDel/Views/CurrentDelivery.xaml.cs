using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.ViewModels;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentDelivery : ContentPage
    {
        public List<CurrentDeliveryViewModel> active_deliveries { get; set; }

        public CurrentDelivery()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();
            BindingContext = this;
            //ListingAPI();
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

            active_deliveries = new List<CurrentDeliveryViewModel>();

            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135467", pickupName = "BEX Express SA", dropoffName = "SPAR: Silver Lakes" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135468", pickupName = "Dawn Wing ", dropoffName = "SPAR: Silver Lakes" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135469", pickupName = "Courierit Pty Ltd Pretoria", dropoffName = "SPAR: Silver Lakes" });


            activeView.ItemsSource = active_deliveries;

        }

        //public async void ListingAPI()
        //{
        //    List<Delivery> data = null;
        //    var httpClientHandler = new HttpClientHandler();
        //    httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
        //    var httpClient = new HttpClient(httpClientHandler);

        //    await Task.Run(async () =>
        //    {
        //        Console.WriteLine("***************");
        //        var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Deliveries?k=UDL2Avv378jBBgd772hFSbbsfwUD");
        //        data = JsonConvert.DeserializeObject<List<Delivery>>(response);
        //        Console.WriteLine("***************");
        //    });

        //    Console.WriteLine(data);
        //    active_deliveries = new List<CurrentDeliveryViewModel>();
        //    foreach (var item in data)
        //    {
        //        if (item.deliveryState == "pending")
        //        {
        //            active_deliveries.Add(new CurrentDeliveryViewModel()
        //            {
        //                deliveryDate = item.deliveryDate,
        //                pickupName = item.deliveryPickupLocation,
        //                dropoffName = item.client
        //            });
        //        }
        //    }

        //    activeView.ItemsSource = active_deliveries;
        //}

        void btnTrack_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new MapPage();
        }

        void btnDeliver_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new CompleteDelivery();
        }

    }
}
