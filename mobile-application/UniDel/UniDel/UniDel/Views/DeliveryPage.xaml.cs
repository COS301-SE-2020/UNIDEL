using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace UniDel.Views
{
    public partial class DeliveryPage : TabbedPage
    {
        public DeliveryPage()
        {
            Children.Add(new CurrentDelivery());
            Children.Add(new CompleteDelivery());

            CurrentPage = Children[0];
            var btn = Children[0].FindByName("pickup_btn");

        }

        public void switchToPending()
        {
            CurrentPage = Children[0];
            
        }

        public void switchToComplete()
        {
            CurrentPage = Children[1];
        }

        string currentPageName = "";
        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();
            App.Nav = CurrentPage.Navigation;
            currentPageName = CurrentPage.Title;
        }

        
    }
}
