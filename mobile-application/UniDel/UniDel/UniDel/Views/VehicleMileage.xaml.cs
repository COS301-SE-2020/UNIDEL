using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using UniDel.Data;
using UniDel.Models;
using UniDel.ViewModels;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class VehicleMileage : ContentPage
    {

        INotificationManager notificationManager;

        Vehicle packet = null;   //new vehicle object to post

        int id;
        public VehicleMileage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void btnLog_Clicked(System.Object sender, System.EventArgs e)
        {
            //make a get request to get all vehicles
            List<Vehicle> vehicles = null;


            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            var httpClient = new HttpClient(httpClientHandler);

            var response = await httpClient.GetStringAsync(Constants.BaseURL + "Vehicles/GetAll?" + Constants.Token);
            vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            foreach (var item in vehicles)
            {
                if (item.VehicleLicensePlate == logPlate.Text)
                {
                    id = item.VehicleID;            //get the id of vehicle to update
                                                    //make a get request for specific vehicle
                    Vehicle vehicle_data = null;


                    var response2 = await httpClient.GetStringAsync(Constants.BaseURL + "Vehicles/" + id + "?" + Constants.Token);
                    vehicle_data = JsonConvert.DeserializeObject<Vehicle>(response2);
                    Console.WriteLine(response2);


                    packet = new Vehicle()
                    {
                        VehicleID = vehicle_data.VehicleID,
                        VehicleMake = vehicle_data.VehicleMake,
                        VehicleLastService = vehicle_data.VehicleLastService,
                        VehicleLicensePlate = vehicle_data.VehicleLicensePlate,
                        VehicleLicenseDiskExpiry = vehicle_data.VehicleLicenseDiskExpiry,
                        VehicleModel = vehicle_data.VehicleModel,
                        VehicleNextMileageService = vehicle_data.VehicleNextMileageService,
                        VehicleVIN = vehicle_data.VehicleVIN,
                        VehicleMileage = int.Parse(logKM.Text),
                        VehicleNextDateService = vehicle_data.VehicleNextDateService
                    };

                }
            }

         //post call
            httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };


            httpClient = new HttpClient(httpClientHandler);
            string json = JsonConvert.SerializeObject(packet);
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");

            //Console.WriteLine(json);
            //API POST CALL TO CREATE
            var response3 = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/Vehicles/Put/" + id + "?" + Constants.Token, content);
            //Console.WriteLine(response3);
            //Console.WriteLine("¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢¢");
            if (response3.StatusCode == HttpStatusCode.NoContent)
            {
                await DisplayAlert("Update success", "Vehicle mileage successfully added, you may proceed to logout", "OK");             
            }
            else
            {
                await DisplayAlert("Update failed", "An error occured while updating the vehicle mileage", "OK");
            }


            Console.WriteLine("--------------------");
            Console.WriteLine(packet.VehicleMileage);
            Console.WriteLine(packet.VehicleNextMileageService);
            if (packet.VehicleMileage > packet.VehicleNextMileageService)
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("yes");
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@");
                notify();
            }
            else
            {
                Console.WriteLine("--------------------");
                Console.WriteLine("no");
                Console.WriteLine("@@@@@@@@@@@@@@@@@@@@@");
            }

            Application.Current.MainPage = new DriverHomePage();

        }


        private void notify()
        {

                notificationManager = DependencyService.Get<INotificationManager>();
                notificationManager.NotificationReceived += (sender, args) =>
                {
                    var evtData = (NotificationEventArgs)args;
                    ShowNotification(evtData.Title, evtData.Message);
                };


                string title = "Vehicle Notification";
                string message = packet.VehicleMake + " " + packet.VehicleModel + " is overdue a service";
                notificationManager.ScheduleNotification(title, message);
 
        }

        private void ShowNotification(string title, string message)
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
