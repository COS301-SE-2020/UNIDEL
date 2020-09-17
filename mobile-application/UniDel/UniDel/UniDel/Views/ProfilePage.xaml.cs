using System;
using System.Collections.Generic;

using Xamarin.Forms;
using UniDel.Models;

namespace UniDel.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        public void Logout(object sender, EventArgs args)
        {
            Session.ClientID = 0;
            Session.UserEmail = null;
            Session.UserToken = null;
            Session.UserType = null;
            Session.DriverID = 0;

            Application.Current.MainPage = new UniDelHome();
        }
    }
}
