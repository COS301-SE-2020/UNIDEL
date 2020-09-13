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
using System.Net.Mail;

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
                if (regCode.Text != "a7be-fb31-ba72-5ce3-a6d2")
                {
                    await DisplayAlert("Registration Error", "Registration Code is incorrect", "OK");
                    return;
                }
                //CHECK COMPLETE: REGISTRATION CODE IS CORRECT

                //WE ASSUME EMAIL IS VALID AND PASSWORDS MATCH EACH OTHER
                //ASSIGN VALUES
                string email = regEmail.Text;
                string pass = regPass.Text;
                string type = "Client";
                string name = regName.Text;
                string phone = regPhone.Text;
                string address = regAddress.Text;

                //HASH THE PASSWORD
                byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(pass);
                HashAlgorithm hashAlg = new SHA256CryptoServiceProvider();
                byte[] salt = hashAlg.ComputeHash(b64pass);
                byte[] finalString = new byte[b64pass.Length + salt.Length];
                for (int i = 0; i < b64pass.Length; i++)
                {
                    finalString[i] = b64pass[i];
                }
                for (int i = 0; i < salt.Length; i++)
                {
                    finalString[b64pass.Length + i] = salt[i];
                }
                string final = Convert.ToBase64String(hashAlg.ComputeHash(finalString));
                //PASSWORD HASHED

                User u = new User();
                u.UserEmail = email;
                u.UserPassword = final;
                u.UserProfilePic = null;
                u.UserType = type;
                u.UserConfirmed = false;
                u.UserToken = Guid.NewGuid().ToString();

                //MAKE POST CALL TO CREATE USER
                List<User> users = null;
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);
                string json = JsonConvert.SerializeObject(u);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                indicator.IsRunning = true;
                indicator.IsVisible = true;

                //API POST CALL TO CREATE
                var response = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/Users/PostUser", content);
                if (response.StatusCode == HttpStatusCode.Created)
                {
                    //MAKE API CALL TO RETRIEVE THE NEWLY POSTED USER
                    var retrievedUsers = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Users/GetAllUsers");
                    users = JsonConvert.DeserializeObject<List<User>>(retrievedUsers);
                    u = users.Where<User>(o => o.UserEmail == email).FirstOrDefault();
                    if (u == null)
                    {
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        await DisplayAlert("Registration failed", "An error occured while creating the User Profile", "OK");
                        return;
                    }

                    //IF WE ARE HERE USER CREATION WAS SUCCESSFUL
                    Client c = new Client();
                    c.ClientName = name;
                    c.ClientTelephone = phone;
                    c.ClientAddress = address;
                    c.UserID = u.UserID;
                    c.User = null;

                    json = JsonConvert.SerializeObject(c);
                    content = new StringContent(json, Encoding.UTF8, "application/json");
                    var clientResponse = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/PostClient", content);
                    
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    if (clientResponse.StatusCode == HttpStatusCode.Created)
                    {
                        //SEND VERIFICATION EMAIL FOR CONFIRMATION
                        string token = u.UserToken;
                        MailMessage mail = new MailMessage();
                        SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                        mail.From = new MailAddress("memoryinjectlamas@gmail.com");
                        mail.To.Add(email);
                        mail.Subject = "UniDel confirmation email";
                        mail.Body = "Your UniDel account has been created. To activate your account click on the following confirmation link \r\n " +
                            "https://www.unideldeliveries.co.za/Account/ConfirmEmail?email=" + email + "&token=" + token;

                        SmtpServer.Port = 587;
                        SmtpServer.Credentials = new System.Net.NetworkCredential("memoryinjectlamas@gmail.com", Session.GPW);
                        SmtpServer.EnableSsl = true;
                        ServicePointManager.ServerCertificateValidationCallback = (message, cert, chain, errors) => { return true; };

                        SmtpServer.Send(mail);

                        await DisplayAlert("Registration Successful", "A verification email has been sent to " + email, "OK");
                        await DisplayAlert("Registration Complete", "You can now log into UniDel", "Proceed to Login");
                        Application.Current.MainPage = new LoginPage();
                        return;
                    }
                    else
                    {
                        await DisplayAlert("Registration Info", "Did not register successfully", "OK");
                        return;
                    }
                }
                else
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Registration failed", "An error occured while creating the User Profile", "OK");
                    return;
                }
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