using System;
using System.Collections.Generic;
using System.Linq;
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

        void OnLoginClicked(object sender, EventArgs args)
        {
            string em = loginEmail.Text;
            string pw = loginPassword.Text;
            if (em == "" || pw == "")
            {
                DisplayAlert("Login Error", "All fields are required", "OK");
            }

            if (em == "fineyouwinallthetime@gmail.com")
            {
                Application.Current.MainPage = new MainPage();
            }
            else
            {
                DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
            }
        }
    }
}