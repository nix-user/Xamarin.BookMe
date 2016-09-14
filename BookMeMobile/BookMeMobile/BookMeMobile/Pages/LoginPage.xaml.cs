using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Android.Provider;
using BookMeMobile.Binding;
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

        public static List<User> Users { get; set; } = new List<User>
        {
            new User() { Id = 1, Password = "1", Login = "User1", FavoriteRoom = "304D", MyRoom = "410" },
            new User() { Id = 2, Password = "2", Login = "User2", FavoriteRoom = "303D", MyRoom = "409" }
        };

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private async void BtnSignIn_OnClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
}