using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookMeMobile.Data;
using BookMeMobile.Interface;
using BookMeMobile.Pages;
using BookMeMobile.Pages.Login;
using BookMeMobile.Resources;
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
                this.MainPage = new NavigationPage(new LoginPage());
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