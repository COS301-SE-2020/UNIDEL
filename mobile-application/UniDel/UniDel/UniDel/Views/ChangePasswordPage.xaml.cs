using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using UniDel.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChangePasswordPage : ContentPage
    {
        public ChangePasswordPage()
        {
            InitializeComponent();
        }

        public async void ChangePass(object sender,EventArgs args)
        {
            try
            {
                if (string.IsNullOrEmpty(oldPass.Text) || string.IsNullOrEmpty(newPass.Text) || string.IsNullOrEmpty(confirmNewPass.Text))
                {
                    await DisplayAlert("Input error", "All fields are required", "Okay");
                    return;
                }

                if (newPass.Text != confirmNewPass.Text)
                {
                    await DisplayAlert("Input error", "New password does not match confirmed password", "Okay");
                    return;
                }

                string pass = oldPass.Text;
                string newP = newPass.Text;

                //IF WE ARE HERE BASIC INPUT VALIDATION HAS SUCCEEDED AND WE CAN PROCEED TO MAKE API CALLS
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Users/GetAllUsers?k=UDL2Avv378jBBgd772hFSbbsfwUD");
                List<User> users = JsonConvert.DeserializeObject<List<User>>(response);
                User u = users.Where(o => o.UserEmail == Session.UserEmail).FirstOrDefault();
                //USER HAS BEEN RETRIEVED

                //HASH OLD PASSWORD
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
                
                if (final == u.UserPassword)
                {
                    //HASH NEW PASSWORD
                    b64pass = System.Text.Encoding.Unicode.GetBytes(newP);
                    salt = hashAlg.ComputeHash(b64pass);
                    finalString = new byte[b64pass.Length + salt.Length];
                    for (int i = 0; i < b64pass.Length; i++)
                    {
                        finalString[i] = b64pass[i];
                    }
                    for (int i = 0; i < salt.Length; i++)
                    {
                        finalString[b64pass.Length + i] = salt[i];
                    }
                    final = Convert.ToBase64String(hashAlg.ComputeHash(finalString));
                    u.UserPassword = final;

                    var json = JsonConvert.SerializeObject(u);
                    StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                    var resp = await httpClient.PostAsync("https://api.unideldeliveries.co.za/api/PutUser/" + u.UserID + "?k=UDL2Avv378jBBgd772hFSbbsfwUD", content);

                    if (resp.StatusCode == HttpStatusCode.NoContent)
                    {
                        await DisplayAlert("Success", "Password Changed", "Okay");
                        base.OnBackButtonPressed();
                    }
                    else
                    {
                        await DisplayAlert("Password Change Failed", "An error occurred that has prevented the password from being changed", "Okay");
                        return;
                    }
                }
                else
                {
                    await DisplayAlert("Password Change Failed", "Old password is incorrect.", "Okay");
                    return;
                }
            }
            catch (Exception e)
            {
                await DisplayAlert("Error", e.Message, "Okay");
                return;
            }
        }

        public void Cancel(object sender, EventArgs args)
        {
            base.OnBackButtonPressed();
        }
    }
}