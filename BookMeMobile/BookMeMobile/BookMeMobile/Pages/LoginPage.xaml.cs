using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using Java.Lang;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class LoginPage : ContentPage
    {
        public List<User> Users { get; set; } = new List<User>() { new User() { Id = 1, Login = "User1" }, new User() { Id = 2, Login = "User2" } };

        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void BtnSignIn_OnClicked(object sender, EventArgs e)
        {
            User user = this.Users.FirstOrDefault(x => x.Login == TextLogin.Text);
            if (user != null)
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
