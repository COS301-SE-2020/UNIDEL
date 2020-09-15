using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class UniDelHome : ContentPage
    {
        public UniDelHome()
        {
            InitializeComponent();
        }

        void customer_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();

        }

        void driver_clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new LoginPage();

        }
    }
}
