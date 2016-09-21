using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages.Login
{
    public partial class LoginPage : ActivityIndicatorPage
    {
        public LoginPage()
        {
            this.InitializeComponent();
            var viewModel = new LoginViewModel() { Navigation = this.Navigation };
            viewModel.ToggleProgressIndicator = this.ToggleProgressIndicator;
            viewModel.ShowInfoMessage = (title, content, cancelText) =>
            {
                this.DisplayAlert(title, content, cancelText);
            };
            this.BindingContext = viewModel;
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }

        private void ToggleProgressIndicator(bool isIndicatorShown)
        {
            if (isIndicatorShown)
            {
                this.ShowActivityIndicator();
            }
            else
            {
                this.HideActivityIndicator();
            }
        }
    }
}