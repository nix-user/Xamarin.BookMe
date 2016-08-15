﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Entity;
using App2.Page;
using Xamarin.Forms;

namespace App2.Pages
{
    public partial class LoginPage : ContentPage
    {
        public List<User> Users { get; set; }=new List<User>() {new User() {Id = 1,Login = "User1"},new User() {Id=2,Login ="User2"} };
        public LoginPage()
        {
            InitializeComponent();
        }
        
        private void BtnSignIn_OnClicked(object sender, EventArgs e)
        {
            User user = Users.FirstOrDefault(x => x.Login == TextLogin.Text);
            if (user!=null)
            {
                Navigation.PushAsync(new SelectPage(user));
            }
            else
            {
                Error.Text = "Такого пользователя нет";
            }
        }
    }
}
