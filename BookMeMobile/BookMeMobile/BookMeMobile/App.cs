using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Data.Concrete;
using BookMeMobile.Enums;
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
            if (Task.Run(async () => await DependencyService.Get<IFileWorker>().ExistsAsync(FileResources.FileName)).Result)
            {
                var selectPage = new SelectPage();
                var navigationService = new NavigationService(selectPage.Navigation);
                selectPage.ViewModel = new SelectViewModel(new ListRoomManager(), navigationService);
                this.MainPage = new NavigationPage(new MasterPage(selectPage));
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
            ProfileService profileService =
                    new ProfileService(
                        new ProfileRepository(new HttpService(new CustomDependencyService(), new HttpClientHandler())));
            Task.Run(async () => await profileService.GetUserData());
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