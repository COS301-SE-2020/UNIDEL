using System;
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
                    var response = await httpClient.GetStringAsync("http://api.unideldeliveries.co.za/api/Users/GetAllUsers?k=UDL2Avv378jBBgd772hFSbbsfwUD");
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

                //CHECK IF LOGIN WAS SUCCESSFUL
                if (u.UserEmail == em && u.UserPassword == final)
                {
                    if (Session.ClientID == -1)
                    {
                        //this means user selected driver
                        var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Drivers?k=UDL2Avv378jBBgd772hFSbbsfwUD");

                        List<Driver> drivers = JsonConvert.DeserializeObject<List<Driver>>(response);

                        Driver d = findDriver(drivers, u.UserID);
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        if (d != null)
                        {
                            Session.ClientID = -1;
                            Session.UserEmail = u.UserEmail;
                            Session.UserToken = u.UserToken;
                            Session.UserType = u.UserType;
                            Session.DriverID = d.DriverID;

                            Application.Current.MainPage = new DriverHomePage();
                        }
                        else
                        {
                            await DisplayAlert("Login Error", "There is no driver registered with these details", "OK");
                            return;
                        }
                    }
                    else
                    {
                        //this means user selected client
                        var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/clients/getallclients?k=UDL2Avv378jBBgd772hFSbbsfwUD");

                        List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response);

                        Client c = findClient(clients, u.UserID);
                        indicator.IsRunning = false;
                        indicator.IsVisible = false;
                        if (c != null)
                        {
                            Session.ClientID = c.ClientID;
                            Session.UserEmail = u.UserEmail;
                            Session.UserToken = u.UserToken;
                            Session.UserType = u.UserType;
                            Session.DriverID = -1;

                            Application.Current.MainPage = new ClientHomePage();
                        } 
                        else
                        {
                            await DisplayAlert("Login Error", "There is no client registered with these details", "OK");
                            return;
                        }
                        
                    }
                }
                else
                {
                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                }
                //////////////////////////////////////////////////////////////////// FROM HERE
                /*
                //If User selected Driver
                if (Session.ClientID == -1)
                {
                    
                } 
                else
                {
                    // if User is of type Client
                    if (u.UserType == "Client")
                    {
                        httpClientHandler = new HttpClientHandler();

                        httpClientHandler.ServerCertificateCustomValidationCallback =
                        (message, cert, chain, errors) => { return true; };

                        httpClient = new HttpClient(httpClientHandler);
                        var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/clients/getallclients?k=UDL2Avv378jBBgd772hFSbbsfwUD");

                        List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response);

                        Client c = findClient(clients, u.UserID);
                        Session.ClientID = c.ClientID;
                    }
                }


                // if User is of type Client
                if (u.UserType == "Client")
                {
                    httpClientHandler = new HttpClientHandler();

                    httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => { return true; };

                    httpClient = new HttpClient(httpClientHandler);
                    var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/clients/getallclients?k=UDL2Avv378jBBgd772hFSbbsfwUD");

                    List<Client> clients = JsonConvert.DeserializeObject<List<Client>>(response);

                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    if (u.UserEmail == em && u.UserPassword == final)
                    {
                        Session.UserEmail = u.UserEmail;
                        Session.UserToken = u.UserToken;
                        Session.UserType = u.UserType;
                        Client c = findClient(clients, u.UserID);
                        Session.ClientID = c.ClientID;
                        Session.DriverID = -1;

                        Application.Current.MainPage = new ClientHomePage();

                    }
                    else
                    {
                        await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                    }
                } 
                else if (u.UserType == "Driver")
                {
                    httpClientHandler = new HttpClientHandler();

                    httpClientHandler.ServerCertificateCustomValidationCallback =
                    (message, cert, chain, errors) => { return true; };

                    httpClient = new HttpClient(httpClientHandler);
                    var response = await httpClient.GetStringAsync("https://api.unideldeliveries.co.za/api/Drivers?k=UDL2Avv378jBBgd772hFSbbsfwUD");

                    List<Driver> drivers = JsonConvert.DeserializeObject<List<Driver>>(response);

                    indicator.IsRunning = false;
                    indicator.IsVisible = false;
                    if (u.UserEmail == em && u.UserPassword == final)
                    {
                        Session.UserEmail = u.UserEmail;
                        Session.UserToken = u.UserToken;
                        Session.UserType = u.UserType;
                        Driver d = findDriver(drivers, u.UserID);
                        Session.ClientID = -1;
                        Session.DriverID = d.DriverID;

                        Application.Current.MainPage = new DriverHomePage();

                    }
                    else
                    {
                        await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                    }
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
                        //Application.Current.MainPage = new ClientHomePage();
                    }
                    else
                    {
                        Session.ClientID = 0;
                        //Application.Current.MainPage = new DriverHomePage();
                    }
                    Application.Current.MainPage = new ClientHomePage();

                }
                else
                {
                    await DisplayAlert("Login Error", "Login failed. Email or password is incorrect", "OK");
                }
                */
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

        private Driver findDriver(List<Driver> c, int userID)
        {
            foreach (Driver u in c)
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