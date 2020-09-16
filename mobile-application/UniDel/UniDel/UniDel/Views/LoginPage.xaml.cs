﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Models;
using Newtonsoft.Json;
using System.Security.Cryptography;
using UniDel.Data;

namespace UniDel.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void OnLoginClicked(object sender, EventArgs args)
        {
            try
            {
                if (loginEmail.Text == null || loginPassword.Text == null || loginEmail.Text == "" || loginPassword.Text == "")
                {
                    await DisplayAlert("Login Error", "All fields are required", "OK");
                    return;
                }

                string em = loginEmail.Text;
                string pw = loginPassword.Text;

                List<User> users = null;
                var httpClientHandler = new HttpClientHandler();
                httpClientHandler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => { return true; };

                var httpClient = new HttpClient(httpClientHandler);

                indicator.IsRunning = true;
                indicator.IsVisible = true;
                await Task.Run(async () =>
                {
                    var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Users/GetAllUsers?"+Constants.Token);
                    users = JsonConvert.DeserializeObject<List<User>>(response);
                });

                if (users == null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Error", "Server error", "OK");
                    return;
                }

                User u = FindUser(users, em);
                if (u == null)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                    return;
                }

                if (u.UserConfirmed == false)
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Error", "User account not confirmed. Please confirm your account via email", "OK");
                    return;
                }

                //APPLICATION ONLY FOR DRIVERS AND CLIENTS THEREFORE ACCESS SHOULD BE DENIED TO ANYONE ELSE
                /*
                if (u.UserType != "Driver" && u.UserType != "Client")
                {
                    await DisplayAlert("Login Error", "User does not have access to this functionality", "OK");
                    return;
                }
                */
                //BUT FOR DEMOING PURPOSES THIS FUNCTIONALITY HAS BEEN ALLOWED

                //HASH PASSWORD
                byte[] b64pass = System.Text.Encoding.Unicode.GetBytes(pw);
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

                // if User is of type Client
                if (u.UserType == "Client")
                {
                    httpClientHandler = new HttpClientHandler();

                    httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => { return true; };

                    httpClient = new HttpClient(httpClientHandler);
                    var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/clients/getclients?"+Constants.Token);

                    List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response);

                    Client c = findClient(clients, u.UserID);
                    Session.ClientID = c.ClientID;
                }

                indicator.IsRunning = false;
                indicator.IsVisible = false;
                if (u.UserEmail == em && u.UserPassword == final)
                {
                    Session.UserEmail = u.UserEmail;
                    Session.UserToken = u.UserToken;
                    Session.UserType = u.UserType;

                    // if User is of type Client
                    if (u.UserType == "Client")
                    {
                        // do nothing, already set
                    }
                    else
                    {
                        Session.ClientID = 0;
                    }

                    Application.Current.MainPage = new DriverHomePage();
                }
                else
                {
                    await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                }
            }
            catch(Exception e)
            {
                indicator.IsRunning = false;
                indicator.IsVisible = false;
                await DisplayAlert("Login Error", e.Message, "OK");
            }
        }

        private Client findClient(List<Client> c, int userID)
        {
            foreach (Client u in c)
            {
                if (u.UserID == userID)
                {
                    return u;
                }
            }
            return null;
        }

        User FindUser(List<User> users,string email)
        {
            foreach (User u in users)
            {
                if (u.UserEmail == email)
                {
                    return u;
                }
            }
            return null;
        }

        public Command RegisterLinkCommand => new Command(() => {
            Application.Current.MainPage = new RegisterPage();
        } );
    }
}