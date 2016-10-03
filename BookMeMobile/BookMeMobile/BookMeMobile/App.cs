using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data.Abstract;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Interface;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
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
                this.navigationService.ShowViewModelAsMainPage<SelectViewModel>(out mainPage);
            }
            else
            {
                this.navigationService.ShowViewModelAsMainPage<LoginViewModel>(out mainPage);
            }

            this.MainPage = mainPage;
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

        private void SetupDependencies()
        {
            App.Container = new UnityContainer();
            App.Container.RegisterType<IAuthService, AuthService>();
            App.Container.RegisterType<IDependencyService, CustomDependencyService>();
            App.Container.RegisterType<IHttpHandler, HttpClientHandler>();
            App.Container.RegisterType<LoginViewModel>();
            App.Container.RegisterType<ListRoomManager>();
            App.Container.RegisterType<SelectViewModel>();
        }
    }
}