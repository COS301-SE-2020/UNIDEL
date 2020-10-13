using Android.Widget;
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
    public partial class ClientHomePage : TabbedPage
    {
        public ClientHomePage()
        {
            InitializeComponent();
        }

        long lastpress;
        protected override bool OnBackButtonPressed()
        {
            long currenttime = DateTime.UtcNow.Ticks / TimeSpan.TicksPerMillisecond;

            if (currenttime - lastpress > 5000)
            {
                lastpress = currenttime;
                if (CurrentPage == Children[0])
                {
                    return base.OnBackButtonPressed();
                }

                if (Navigation.NavigationStack.Count == 1)
                {
                    CurrentPage = this.Children[0];
                    return true;
                }
                else
                {
                    return base.OnBackButtonPressed();
                }
            }
            else
            {
                return base.OnBackButtonPressed();
            }
        }
    }
}
