using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL.Concrete;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.Pages.Login
{
    public partial class LoginPage : BasePage
    {
        public LoginPage()
        {
            this.InitializeComponent();
            //var viewModel = new LoginViewModel(new AccountService()) { Navigation = this.Navigation };

            this.SetUpActivityIndicator(this.loader, this.rootLayout);
        }

        protected override void OnViewModelSet()
        {
            base.OnViewModelSet();
            this.SetUpViewModelSubscriptions(this.ViewModel);
            this.BindingContext = this.ViewModel;
        }
    }
}