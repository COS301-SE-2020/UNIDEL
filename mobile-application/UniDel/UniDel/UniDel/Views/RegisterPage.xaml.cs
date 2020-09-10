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
    public partial class RegisterPage : ContentPage
    {
        public RegisterPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        public async void onRegisterClicked(object sender, EventArgs args)
        {
            try
            {

            }
            catch(Exception e)
            {

            }
        }

        public Command LoginLinkCommand => new Command(() => {
            Application.Current.MainPage = new LoginPage();
        });
    }
}