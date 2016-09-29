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
        public static UnityContainer Container { get; set; }

        public App()
        {
            INavigation navigation;
            this.SetupDependencies();

            if (Task.Run(async () => await DependencyService.Get<IFileWorker>().ExistsAsync(FileResources.FileName)).Result)
            {
                var selectPage = new SelectPage();
                navigation = selectPage.Navigation;
                var navigationService = new NavigationService(navigation);
                selectPage.ViewModel = new SelectViewModel(new ListRoomManager(), navigationService);
                this.MainPage = new NavigationPage(selectPage);
            }
            else
            {
                var navigationService = new NavigationService();
                Page mainPage;
                navigationService.ShowViewModelAsMainPage<LoginViewModel>(out mainPage);
                this.MainPage = mainPage;
                /*var loginPage = new LoginPage();
                var accountService = new AuthService(new CustomDependencyService(), new HttpClientHandler());
                navigation = loginPage.Navigation;
                var navigationService = new NavigationService(navigation);
                loginPage.ViewModel = new LoginViewModel(accountService, navigationService);
                this.MainPage = new NavigationPage(loginPage);*/
            }
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
            //App.Container.RegisterType<INavigationService, NavigationService>(new InjectionConstructor(navigation));
            App.Container.RegisterType<LoginViewModel>();
        }
    }
}