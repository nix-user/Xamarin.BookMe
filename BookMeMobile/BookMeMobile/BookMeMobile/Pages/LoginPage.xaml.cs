using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Provider;
using BookMeMobile.Binding;
using BookMeMobile.BL;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyUserIsNotExist = "Логин или пароль введены неверно";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string Ok = "Ok";

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void BtnSignIn_OnClicked(object sender, EventArgs e)
        {
            AccountService service = new AccountService();
            User user = new User()
            {
                Login = TextLogin.Text,
                Password = textPassword.Text
            };
            var request = await service.GetTocken(user);
            switch (request.StatusCode)
            {
                case StatusCode.Ok:
                    {
                        await DependencyService.Get<IFileWork>().SaveTextAsync(request.Token);
                        await this.Navigation.PushAsync(new MainPage(new SelectPage()));
                        break;
                    }

                case StatusCode.NoInternet:
                    {
                        await this.DisplayAlert(HeadError, BodyInternetIsNotExist, Ok);
                        break;
                    }

                case StatusCode.Error:
                    {
                        await this.DisplayAlert(HeadError, BodyUserIsNotExist, Ok);
                        break;
                    }
            }
        }
    }
}