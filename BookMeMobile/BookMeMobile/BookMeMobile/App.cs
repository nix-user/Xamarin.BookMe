using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BookMeMobile.Interface;
using BookMeMobile.Pages;
using Xamarin.Forms;

namespace BookMeMobile
{
    public class App : Application
    {
        public App()
        {
            bool b = DependencyService.Get<IFileWork>().ExistsAsync().Result;
            if (b)
            {
                this.MainPage = new NavigationPage(new LoginPage());
                DependencyService.Get<IFileWork>().DeleteAsync();
            }
            else
            {
                this.MainPage = new NavigationPage(new MainPage());
                DependencyService.Get<IFileWork>().SaveTextAsync("124124");
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