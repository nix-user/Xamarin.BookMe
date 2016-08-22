using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using Javax.Security.Auth;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ProfilePage : ContentPage
    {
        private ProfileViewModel profileViewModel;

        public ProfilePage()
        {
            this.BindingContext = new ProfileViewModel(SelectPage.CurrentUser);
            this.InitializeComponent();
        }
    }
}