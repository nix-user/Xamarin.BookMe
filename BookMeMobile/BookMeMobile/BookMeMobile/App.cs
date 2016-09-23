using BookMeMobile.BL.Concrete;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Interface;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile
{
    public class App : Application
    {
        public App()
        {
            if (DependencyService.Get<IFileWorker>().ExistsAsync(FileResources.FileName).Result)
            {
                this.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                var loginPage = new LoginPage();
                var accountService = new AuthService(new CustomDependencyService(), new HttpClientHandler());
                var navigationService = new NavigationService(loginPage.Navigation);
                loginPage.ViewModel = new LoginViewModel(accountService, navigationService);
                this.MainPage = new NavigationPage(loginPage);
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
    }
}