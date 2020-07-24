using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Services;
using UniDel.Views;

namespace UniDel
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
