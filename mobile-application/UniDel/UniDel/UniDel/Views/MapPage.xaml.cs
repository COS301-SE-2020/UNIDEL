using System;
using System.Collections.Generic;
using RestSharp;
using Xamarin.Forms;

namespace UniDel.Views
{
    public partial class MapPage : ContentPage
    {
        private static RestClient client = new RestClient("https://unidelapi.azurewebsites.net/api/");

        public MapPage()
        {
            InitializeComponent();
        }

        void viewMap(object sender, EventArgs e)
        {
            RestRequest request = new RestRequest("Default", Method.GET);
            //IRestResponse<List<string>> K = client.Execute<List<string>>(request);
            IRestResponse<string> K = client.Execute<string>(request);
            //String K = "Khakhu";
            (sender as Button).Text = K.ToString();
        }

    }
}
