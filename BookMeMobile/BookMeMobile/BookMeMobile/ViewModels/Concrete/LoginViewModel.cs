using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    internal class LoginViewModel : BaseViewModel
    {
        private string login;
        private string password;

        public ICommand SignInCommand { get; private set; }

        public LoginViewModel()
        {
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
            AccountService service = new AccountService();
            var operationStatus = await this.ExecuteOperation(async () => await service.GetTocken(new User() { Login = this.Login, Password = this.Password }));
            if (operationStatus == StatusCode.Ok)
            {
                await this.Navigation.PushAsync(new MainPage(new SelectPage()));
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