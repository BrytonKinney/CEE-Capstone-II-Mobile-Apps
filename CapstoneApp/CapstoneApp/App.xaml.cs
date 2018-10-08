using CapstoneApp.Views;
using Shared.Services.Implementations;
using Shared.Services.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace CapstoneApp
{
    public partial class App : Application
    {
        private static LightInject.ServiceContainer _container;
        
        private static void RegisterServices()
        {
            Container.Register<IXmlRssFeedParser, XmlRssFeedParser>(new LightInject.PerContainerLifetime());
            Container.Register<IRssFeedReader, RssFeedReader>(new LightInject.PerContainerLifetime());
            Container.Register<IDatabaseProvider, DatabaseProvider>(new LightInject.PerContainerLifetime());
        }

        public static LightInject.ServiceContainer Container
        {
            get
            { 
                if(_container == null)
                {
                    _container = new LightInject.ServiceContainer();
                    RegisterServices();
                }
                return _container;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
