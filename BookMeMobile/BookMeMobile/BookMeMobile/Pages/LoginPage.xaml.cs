using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Provider;
using Android.Widget;
using BookMeMobile.Binding;
using BookMeMobile.BL;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class LoginPage : ActivityIndicatorPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyUserIsNotExist = "Логин или пароль введены неверно";
        private const string BodyServerError = "Ошибка на стороне сервера";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string Ok = "Ok";

        public LoginPage()
        {
            this.InitializeComponent();
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }

        private async void BtnSignIn_OnClicked(object sender, EventArgs e)
        {
            AccountService service = new AccountService();
            User user = new User()
            {
                Login = TextLogin.Text,
                Password = textPassword.Text
            };

            var request = default(StatusCode);
            await this.PerformWithActivityIndicator(async () => request = await service.GetTocken(user));

            switch (request)
            {
                case StatusCode.Ok:
                    {
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
                        await this.DisplayAlert(HeadError, BodyServerError, Ok);
                        break;
                    }

                case StatusCode.NoAuthorize:
                    {
                        await this.DisplayAlert(HeadError, BodyUserIsNotExist, Ok);
                        break;
                    }
            }
        }
    }
}