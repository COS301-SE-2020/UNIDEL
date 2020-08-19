using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        public async void OnLoginClicked(object sender, EventArgs args)
        {
            try
            {
                if (loginEmail.Text == null || loginPassword.Text == null)
                {
                    await DisplayAlert("Login Error", "All fields are required", "OK");
                    return;
                }

                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                indicator.IsRunning = true;
                indicator.IsVisible = true;
                await Task.Run(async () =>
                {
                    var response = await httpClient.GetAsync("http://api.unideldeliveries.co.za/api/Deliveries");
                });

                string em = loginEmail.Text;
                string pw = loginPassword.Text;

                if (em == "fineyouwinallthetime@gmail.com")
                {
                    Application.Current.MainPage = new MainPage();
                }
                else
                {
                    await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                }
                indicator.IsRunning = false;
                indicator.IsVisible = false;
            }
            catch(Exception e)
            {

            }
        }
    }
}