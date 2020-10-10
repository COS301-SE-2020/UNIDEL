using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using UniDel.Models;
using UniDel.ViewModels;
using System.Net.Http;

namespace UniDel.Views
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : MasterDetailPage
    {
        Dictionary<int, NavigationPage> MenuPages = new Dictionary<int, NavigationPage>();

        INotificationManager notificationManager;

        public MainPage()
        { 
            InitializeComponent();

            MasterBehavior = MasterBehavior.Popover;

            MenuPages.Add((int)MenuItemType.Browse, (NavigationPage)Detail);

            notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.NotificationReceived += (sender, args) =>
            {
                var evtData = (NotificationEventArgs)args;
                ShowNotification(evtData.Title, evtData.Message);
            };

            
            string title = "Vehicle Notification";
            string message = "Mercedes-Benz C200 is overdue a service";
            notificationManager.ScheduleNotification(title, message);
        }

        void ShowNotification(string title, string message)
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                var msg = new Label()
                {
                    Text = $"Notification Received:\nTitle: {title}\nMessage: {message}"
                };
                //stackLayout.Children.Add(msg);
            });
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
                    case (int)MenuItemType.Logout:
                        Session.UserEmail = null;
                        Session.UserToken = null;
                        Session.UserType = null;
                        Application.Current.MainPage = new UniDelHome();
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