using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.ViewModels;
using System.Collections.ObjectModel;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentDelivery : ContentPage
    {
        public ObservableCollection<CurrentDeliveryViewModel> active_deliveries { get; set; }

        public CurrentDelivery()
        {
            InitializeComponent();
            Navigation.PopToRootAsync();
            BindingContext = this;

            //apidata = [{ }]
            //foreach (x) {
            //    complete_deliveries.Add(new CompleteDeliveryViewModel
            //    { deliveryID = "Delivery ID: "+ X.id,
            //        pickupName = "Pickup : " + X.pickupname,
            //        dropoffName = "Drop Off : " + X.dropoffname });

            //}

            active_deliveries = new ObservableCollection<CurrentDeliveryViewModel>();

            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135467", pickupName = "BEX Express SA", dropoffName = "SPAR: Silver Lakes" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135468", pickupName = "Dawn Wing ", dropoffName = "SPAR: Silver Lakes" });
            active_deliveries.Add(new CurrentDeliveryViewModel
            { deliveryID = "Delivery ID : 135469", pickupName = "Courierit Pty Ltd Pretoria", dropoffName = "SPAR: Silver Lakes" });


            activeView.ItemsSource = active_deliveries;

        }

        async void Add_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new QRScanningPage());
        }

        async void Collect_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeliveryDetails());
        }

    }
}
