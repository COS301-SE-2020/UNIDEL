using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using UniDel.Data;
using UniDel.Models;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class VehicleMileage : ContentPage
    {
        //public string StrNumber
        //{
        //    get
        //    {
        //        if (Kilos == null)
        //            return "";
        //        else
        //            return Kilos.ToString();
        //    }

        //    set
        //    {
        //        try
        //        {
        //            Kilos = int.Parse(value);
        //        }
        //        catch
        //        {
        //            Kilos = null;
        //        }
        //    }
        //}

        public VehicleMileage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void btnLog_Clicked(System.Object sender, System.EventArgs e)
        {
            //make a get request to get all vehicles
            //List<Vehicle> vehicles= null;
            //Vehicle data = null;
            //var httpClientHandler = new HttpClientHandler();
            //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };
            //var httpClient = new HttpClient(httpClientHandler);

            //var response = await httpClient.GetStringAsync(Constants.BaseURL + "Vehicles/GetAll?" + Constants.Token);
            //vehicles = JsonConvert.DeserializeObject<List<Vehicle>>(response);
            //foreach (var item in vehicles)
            //{
            //    if (item.VehicleLicensePlate == logPlate.Text)
            //    {
            //        int id = item.VehicleID;
            //        Vehicle vehicle_data = null;
            //        var response2 = await httpClient.GetStringAsync(Constants.BaseURL + "Clients/" + id + "?" + Constants.Token);
            //        vehicle_data = JsonConvert.DeserializeObject<Vehicle>(response2);


            //        data.Add(new Vehicle()
            //        {
            //            VehicleID = vehicle_data.VehicleID,
            //            VehicleMake = vehicle_data.VehicleMake,
            //            VehicleLastService = vehicle_data.VehicleLastService,
            //            VehicleLicensePlate = vehicle_data.VehicleLicensePlate,
            //            VehicleLicenseDiskExpiry = vehicle_data.VehicleLicenseDiskExpiry,
            //            VehicleModel = vehicle_data.VehicleModel,
            //            VehicleNextMileageService = vehicle_data.VehicleNextMileageService,
            //            VehicleVIN = vehicle_data.VehicleVIN,
            //            VehicleMileage = int(logKM.Text),
            //            VehicleNextDateService = vehicle_data.VehicleNextDateService
            //        });
            //    }
            //}
            ////post call
            //Vehicle packet 
            //httpClientHandler = new HttpClientHandler();
            //httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            //httpClient = new HttpClient(httpClientHandler);
            //string json = JsonConvert.SerializeObject(packet);
            //StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            ////indicator.IsRunning = true;
            ////indicator.IsVisible = true;

            ////API POST CALL TO CREATE
            //var response2 = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/Deliveries/PutDelivery/" + QR_ID_Scanned + "?k=UDL2Avv378jBBgd772hFSbbsfwUD", content);
            //if (response2.StatusCode == HttpStatusCode.NoContent)
            //{
            //    //await DisplayInfoChangedEventArgs("Confirmation in progress");
            //    //await DisplayAlert("Confirming", "Confirmation in progress", "OK");
            //    return;
            //}
            //else
            //{
            //    await DisplayAlert("Confirmation failed", "An error occured while confirming your package", "OK");
            //}

        }
    }
}
