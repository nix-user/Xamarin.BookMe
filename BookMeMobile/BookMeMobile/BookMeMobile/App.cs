using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.BL.Concrete.Fake;
using BookMeMobile.Data;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Data.FakeRepository;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Interface;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Concrete;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace BookMeMobile
{
    public class App : Application
    {
        private INavigationService navigationService;

        public static UnityContainer Container { get; set; }

        public App()
        {
            this.navigationService = new NavigationService();
            this.SetupDependencies();
            Page mainPage;

            if (Task.Run(async () => await DependencyService.Get<IFileWorker>().ExistsAsync(FileResources.FileName)).Result)
            {
                mainPage = this.navigationService.ShowViewModelAsMainPageWithMenu<SelectViewModel>();
            }
            else
            {
                mainPage = this.navigationService.ShowViewModelAsMainPage<LoginViewModel>();
            }

            this.MainPage = mainPage;
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private void SetupDependencies()
        {
            App.Container = new UnityContainer();
            App.Container.RegisterType<IAuthService, FakeAuthService>();
            App.Container.RegisterType<IDependencyService, CustomDependencyService>();
            App.Container.RegisterType<IHttpHandler, HttpClientHandler>();
            App.Container.RegisterType<LoginViewModel>();
            App.Container.RegisterType<ListRoomManager>();
            App.Container.RegisterType<SelectViewModel>();
            App.Container.RegisterType<IProfileService, ProfileService>();
            App.Container.RegisterType<IProfileRepository, FakeProfileRepository>();
            App.Container.RegisterType<IHttpService, HttpService>();
            App.Container.RegisterType<IDependencyService, CustomDependencyService>();
            App.Container.RegisterType<IHttpHandler, HttpClientHandler>();
        }
    }
}