using System;
using System.Collections.Generic;
using UniDel.ViewModels;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class ReviewPage : ContentPage
    {
        private List<CurrentDeliveryViewModel> done_deliveries;

        public ReviewPage()
        {
            InitializeComponent();
        }

        public ReviewPage(List<CurrentDeliveryViewModel> done_deliveries)
        {
            this.done_deliveries = done_deliveries;
        } 

        void OnSwitchToggled(object sender, ToggledEventArgs args)
        {
            pickDate.IsVisible = true;
        }

        void OnDateSelected(object sender, DateChangedEventArgs args)
        {
            foreach (var item in done_deliveries)
            {
                

            }

        }

        void back_Clicked(System.Object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new DriverHomePage();
        }

    }
}
