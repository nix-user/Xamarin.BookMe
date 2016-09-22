using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Data;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.Interface;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile
{
    public class App : Application
    {
        public App()
        {
            if (DependencyService.Get<IFileWorker>().ExistsAsync().Result)
            {
                this.MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                var loginPage = new LoginPage();
                var accountService = new AuthService();
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