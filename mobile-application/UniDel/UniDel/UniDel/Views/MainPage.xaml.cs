﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using UniDel.Models;

namespace UniDel.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();
        public MainPage()
        {
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);
        }

        public async Task NavigateFromMenu(int id)
        {
            if (!MenuPages.ContainsKey(id))
            {
                switch (id)
                {
                    case (int)MenuItemType.QRCode:
                        MenuPages.Add(id, new NavigationPage(new QRScanningPage()));
                        break;
                    case (int)MenuItemType.EndCustomerQRCode:
                        MenuPages.Add(id, new NavigationPage(new EndCustomerQRScanningPage()));
                        break;
                    case (int)MenuItemType.Map:
                        MenuPages.Add(id, new NavigationPage(new MapPage()));
                        break;
                    case (int)MenuItemType.Deliveries:
                        MenuPages.Add(id, new NavigationPage(new DeliveryPage()));
                        break;
                    case (int)MenuItemType.Logout:
                        Session.UserEmail = null;
                        Session.UserToken = null;
                        Session.UserType = null;
                        Application.Current.MainPage = new LoginPage();
                        return;
                }
            }

            var newPage = MenuPages[id];

            if (newPage != null && Detail != newPage)
            {
                Detail = newPage;

                if (Device.RuntimePlatform == Device.Android)
                    await Task.Delay(100);

                IsPresented = false;
            }
        }
    }
}