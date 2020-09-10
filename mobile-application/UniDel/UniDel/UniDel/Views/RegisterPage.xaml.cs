using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

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
                if (regName.Text == null || regName.Text == "")
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
                if (regPhone.Text != "a7be-fb31-ba72-5ce3-a6d2")
                {
                    await DisplayAlert("Registration Error", "Registration Code is incorrect", "OK");
                    return;
                }
                //CHECK COMPLETE: REGISTRATION CODE IS CORRECT

                //WE ASSUME EMAIL IS VALID AND PASSWORDS MATCH EACH OTHER

                

            }
            catch(Exception e)
            {

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