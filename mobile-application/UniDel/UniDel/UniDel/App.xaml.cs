using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using UniDel.Services;
using UniDel.Views;
using UniDel.Data;
using System.IO;

[assembly: ExportFont("Jura-VariableFont_wght.ttf", Alias = "UnidelFont")]

namespace UniDel
{
    
    public partial class App : Application
    {
        static Database _database;

        public static Database Database
        {
            get
            {
                if (_database == null)
                {
                    //_database = new Database(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Notes.db3"));
                }
                return _database;
            }
        }

        public static App Instance;
        readonly Image image = new Image();
        public static INavigation Nav { get; set; }
        public App()
        {
            InitializeComponent();
            DependencyService.Register<MockDataStore>();
            MainPage = new UniDelHome();

            Instance = this;
            /*var myNavigationPage = new NavigationPage(new CurrentDelivery());
            Nav = myNavigationPage.Navigation;*/
        }

        public event Action ShouldTakePicture = () => { };

        public void ShowImage(string filepath)
        {
            image.Source = ImageSource.FromFile(filepath);
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
