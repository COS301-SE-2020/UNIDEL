using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Services;
using UniDel.Views;
[assembly: ExportFont("Jura-VariableFont_wght.ttf", Alias = "UnidelFont")]

namespace UniDel
{
    public partial class App : Application
    {

        public static INavigation Nav { get; set; }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new LoginPage();

            /*var myNavigationPage = new NavigationPage(new CurrentDelivery());
            Nav = myNavigationPage.Navigation;*/
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
