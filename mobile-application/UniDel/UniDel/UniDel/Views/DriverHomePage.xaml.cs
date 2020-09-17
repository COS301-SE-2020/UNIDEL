using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Models;
using UniDel.ViewModels;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DriverHomePage : TabbedPage
    {
        INotificationManager notificationManager;
        public DriverHomePage()
        {
            InitializeComponent();

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
    }
}
