//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: global::Xamarin.Forms.Xaml.XamlResourceIdAttribute("UniDel.Views.MyPage.xaml", "Views/MyPage.xaml", typeof(global::UniDel.Views.CurrentDelivery))]

namespace UniDel.Views {
    
    
    [global::Xamarin.Forms.Xaml.XamlFilePathAttribute("Views\\MyPage.xaml")]
    public partial class CurrentDelivery : global::Xamarin.Forms.ContentPage {
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ContentPage ActiveDeliveries;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ActivityIndicator indicator;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private global::Xamarin.Forms.ListView activeView;
        
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Xamarin.Forms.Build.Tasks.XamlG", "2.0.0.0")]
        private void InitializeComponent() {
            global::Xamarin.Forms.Xaml.Extensions.LoadFromXaml(this, typeof(CurrentDelivery));
            ActiveDeliveries = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ContentPage>(this, "ActiveDeliveries");
            indicator = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ActivityIndicator>(this, "indicator");
            activeView = global::Xamarin.Forms.NameScopeExtensions.FindByName<global::Xamarin.Forms.ListView>(this, "activeView");
        }
    }
}