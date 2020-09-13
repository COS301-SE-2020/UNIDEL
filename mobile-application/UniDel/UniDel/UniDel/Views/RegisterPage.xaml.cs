using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterPage : ContentPage
    {
        private bool[] formChecks = { false,false };

        private bool allChecksClear()
        {
            return true && formChecks[0] && formChecks[1];
        }

        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void OnRegisterClicked(object sender, EventArgs args)
        {
            try
            {
                //CHECK IF ALL FIELDS HAVE INPUT//
                /*if (regName.Text == null || regName.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regEmail.Text == null || regEmail.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regPhone.Text == null || regPhone.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regAddress.Text == null || regAddress.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regCode.Text == null || regCode.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regPass.Text == null || regPass.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }

                if (regPassConfirm.Text == null || regPassConfirm.Text == "")
                {
                    await DisplayAlert("Registration Error", "All fields are required", "OK");
                    return;
                }
                //CHECK COMPLETE: ALL FIELDS HAVE INPUT//

                //CHECK IF REGISTRATION CODE MATCHES
                if (regCode.Text != "a7be-fb31-ba72-5ce3-a6d2")
                {
                    await DisplayAlert("Registration Error", "Registration Code is incorrect", "OK");
                    return;
                }*/
                //CHECK COMPLETE: REGISTRATION CODE IS CORRECT

                //WE ASSUME EMAIL IS VALID AND PASSWORDS MATCH EACH OTHER
                //ASSIGN VALUES
                string email = regEmail.Text;
                string pass = regPass.Text;
                string type = "Client";
                string name = regName.Text;
                string phone = regPhone.Text;
                string address = regAddress.Text;

                /*User u = new User();
                u.UserEmail = email;*/
                //LEFT IT HERE
                Client c = new Client();
                c.ClientID = 61;
                c.ClientName = "90 Degrees Shop";
                c.ClientTelephone = "09876374";
                c.ClientAddress = "South Park";
                c.UserID = 3;
                c.User = null;

                //NOW WE MAKE THE API POST CALLS
                List<User> users = null;
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);
                string json = JsonConvert.SerializeObject(c);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                indicator.IsRunning = true;
                indicator.IsVisible = true;

                var response = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/PostClient", content);
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    await DisplayAlert("Registration Info", "Successfully Registered", "OK");
                    return;
                }
                else
                {
                    await DisplayAlert("Registration Info", "Did not register successfully", "OK");
                    return;
                }

                /*await Task.Run(async () =>
                {
                    var response = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/PostClient", content);
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    if (response.IsSuccessStatusCode)
                    {
                        await DisplayAlert("Registration Info", "Successfully Registered", "OK");
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Registration Info", "Did not register successfully", "OK");
                        return;
                    }
                });*/
            }
            catch(Exception e)
            {
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                await DisplayAlert("Server Error", e.Message, "OK");
                return;
            }
        }

        public void OnRegPassConfirmTextChanged(object sender, EventArgs args)
        {
            try
            {
                if (regPassConfirm.Text == regPass.Text)
                {
                    regPassConfirm.BackgroundColor = Color.FromHex("#88fc8c");
                    formChecks[1] = true;
                    if (allChecksClear()) regButton.IsEnabled = true;
                    else regButton.IsEnabled = false;
                }
                else
                {
                    regPassConfirm.BackgroundColor = Color.FromHex("#fc8888");
                    formChecks[1] = false;
                    if (allChecksClear()) regButton.IsEnabled = true;
                    else regButton.IsEnabled = false;
                }
            }
            catch(Exception e)
            {

            }
        }

        public void OnRegEmailTextChanged(object sender, EventArgs args)
        {
            if (Patterns.EmailAddress.Matcher(regEmail.Text).Matches())
            {
                regEmail.BackgroundColor = Color.FromHex("#88fc8c");
                formChecks[0] = true;
                if (allChecksClear()) regButton.IsEnabled = true;
                else regButton.IsEnabled = false;
            }
            else
            {
                regEmail.BackgroundColor = Color.FromHex("#fc8888");
                formChecks[0] = false;
                regButton.IsEnabled = false;
            }
            
        }

        public Command LoginLinkCommand => new Command(() => {
            Application.Current.MainPage = new LoginPage();
        });
    }
}