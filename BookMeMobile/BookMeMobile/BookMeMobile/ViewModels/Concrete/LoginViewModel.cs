using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Infrastructure.Concrete;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class LoginViewModel : BaseViewModel
    {
        private string login;
        private string password;

        private IAccountService accountService;

        public ICommand SignInCommand { get; private set; }

        public LoginViewModel(IAccountService accountService, INavigationService navigationService) : base(navigationService)
        {
            this.accountService = accountService;
            this.SignInCommand = new Command(this.SignIn);
        }

        public string Login
        {
            get { return this.login; }
            set
            {
                this.login = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return this.password; }
            set
            {
                this.password = value;
                this.OnPropertyChanged();
            }
        }

        private async void SignIn()
        {
            var operationStatus = await this.ExecuteOperation(async () => await this.accountService.GetToken(new User() { Login = this.Login, Password = this.Password }));
            if (operationStatus == StatusCode.Ok)
            {
                this.NavigationService.ShowViewModel(new LoginViewModel(this.accountService, this.NavigationService));
                return;
            }

            if (operationStatus == StatusCode.NoAuthorize)
            {
                this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.WrongLoginOrPassword);
                return;
            }

            this.ShowErrorMessage(operationStatus);
        }
    }
}