﻿using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Models;

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
            Session.DriverID = -1;
            Application.Current.MainPage = new LoginPage();

        }

        void driver_clicked(object sender, EventArgs e)
        {
            Session.ClientID = -1;
            Application.Current.MainPage = new LoginPage();

        }
    }
}
