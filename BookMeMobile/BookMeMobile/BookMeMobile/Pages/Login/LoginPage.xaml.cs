using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages.Login
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            this.InitializeComponent();
            var viewModel = new LoginViewModel() { Navigation = this.Navigation };
            this.SetUpViewModelSubscriptions(viewModel);
            this.BindingContext = viewModel;
            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }
    }
}