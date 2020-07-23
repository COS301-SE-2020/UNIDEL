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

            /*WebView webView = new WebView
            {
                /*Source = new UrlWebViewSource
                {
                    Url = "https://memoryinjectllamas.carto.com/builder/9c8296c3-1e37-43b0-8327-5ec2a868dd57/embed",
                }
                Source= "https://memoryinjectllamas.carto.com/builder/9c8296c3-1e37-43b0-8327-5ec2a868dd57/embed",
            };*/
        }

        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
            labelLoading.IsVisible = true;
        }

        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
            labelLoading.IsVisible = false;
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
