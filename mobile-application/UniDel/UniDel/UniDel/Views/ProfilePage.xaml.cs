using System;
using System.Collections.Generic;

using Xamarin.Forms;
using UniDel.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using System.Net;

namespace UniDel.Views
{
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
            LoadData();
        }
        Client c;
        Driver d;
        Entry address;
        Entry tel;
        Button btnSave;
        Button btnLogout;
        Button btnChange;

        async void LoadData()
        {
            var httpClientHandler = new HttpClientHandler();
            httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

            var httpClient = new HttpClient(httpClientHandler);
            

            if (Session.DriverID == -1)
            {
                var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/GetClient/" + Session.ClientID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                c = JsonConvert.DeserializeObject<Client>(response);
                address = new Entry();
                tel = new Entry();
                btnSave = new Button();
                btnSave.Text = "Save Changes";
                btnSave.WidthRequest = 100;
                btnSave.BackgroundColor = Color.FromHex("#26C485");
                btnSave.TextColor = Color.White;
                btnSave.FontAttributes = FontAttributes.Bold;
                btnSave.FontFamily = "UnidelFont";
                btnLogout = new Button();
                btnLogout.WidthRequest = 100;
                btnLogout.Text = "Logout";
                btnLogout.BackgroundColor = Color.Crimson;
                btnLogout.TextColor = Color.White;
                btnLogout.FontAttributes = FontAttributes.Bold;
                btnLogout.FontFamily = "UnidelFont";
                btnChange = new Button();
                btnChange.WidthRequest = 100;
                btnChange.Text = "Change Password";
                btnChange.BackgroundColor = Color.FromHex("#26C485");
                btnChange.TextColor = Color.White;
                btnChange.FontAttributes = FontAttributes.Bold;
                btnChange.FontFamily = "UnidelFont";

                btnSave.Clicked += Save;
                btnLogout.Clicked += Logout;
                btnChange.Clicked += ChangePassPage;

                //try
                //{
                address.Text = c.ClientAddress;
                    tel.Text = c.ClientTelephone;
                //}
                //catch (Exception clientFillin)
                //{

                //}
                stackLayout.Children.Add(tel);
                stackLayout.Children.Add(address);
                stackLayout.Children.Add(btnChange);
                stackLayout.Children.Add(btnSave);
                stackLayout.Children.Add(btnLogout);
            }
            else
            {
                try
                {
                    try
                    {
                        var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Drivers/" + Session.DriverID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                        d = JsonConvert.DeserializeObject<Driver>(response);
                        //await DisplayAlert("Driver", d.DriverID.ToString(), "OK");
                    }
                    catch (Exception DriverAPI)
                    {
                        await DisplayAlert("DriverAPI Error", DriverAPI.Message, "OK");
                    }
                    tel = new Entry();
                    btnSave = new Button();
                    btnSave.Text = "Save Changes";
                    btnSave.WidthRequest = 100;
                    btnSave.BackgroundColor = Color.FromHex("#26C485");
                    btnSave.TextColor = Color.White;
                    btnSave.FontAttributes = FontAttributes.Bold;
                    btnSave.FontFamily = "UnidelFont";
                    btnLogout = new Button();
                    btnLogout.WidthRequest = 100;
                    btnLogout.Text = "Logout";
                    btnLogout.TextColor = Color.White;
                    btnLogout.BackgroundColor = Color.Crimson;
                    btnLogout.FontAttributes = FontAttributes.Bold;
                    btnLogout.FontFamily = "UnidelFont";
                    btnChange = new Button();
                    btnChange.WidthRequest = 100;
                    btnChange.Text = "Change Password";
                    btnChange.BackgroundColor = Color.FromHex("#26C485");
                    btnChange.TextColor = Color.White;
                    btnChange.FontAttributes = FontAttributes.Bold;
                    btnChange.FontFamily = "UnidelFont";

                    btnSave.Clicked += Save;
                    btnLogout.Clicked += Logout;
                    btnChange.Clicked += ChangePassPage;

                    //try
                    //{
                        tel.Text = d.DriverCellphone;
                    //}
                    stackLayout.Children.Add(tel);
                    stackLayout.Children.Add(btnChange);
                    stackLayout.Children.Add(btnSave);
                    stackLayout.Children.Add(btnLogout);
                }
                catch (Exception o)
                {
                    await DisplayAlert("Profile Page Error", o.Message, "OK");
                }
            }
            
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

        async void Save(object sender, EventArgs args)
        {
            if (Session.DriverID == -1)
            {
                c.ClientAddress = address.Text;
                c.ClientTelephone = tel.Text;

                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                var json = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/PutClient/" + Session.ClientID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD", content);

                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    btnSave.IsEnabled = false;
                }
                else
                {
                    await DisplayAlert("Update Error", "Details not saved", "OK");
                    return;
                }
            }
            else
            {
                btnSave.IsEnabled = false;
            }

        }

        public void ChangePassPage(object sender, EventArgs args)
        {
            Navigation.PushAsync(new ChangePasswordPage());
        }
    }
}
