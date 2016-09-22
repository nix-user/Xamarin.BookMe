using System.Windows.Input;
using BookMeMobile.BL.Concrete;
using BookMeMobile.Enums;
using BookMeMobile.Model.Login;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    internal class LoginViewModel : BaseViewModel
    {
        private readonly AuthService authService;
        private LoginModel model;

        public ICommand SignInCommand { get; private set; }

        public LoginViewModel()
        {
            this.SignInCommand = new Command(this.SignIn);
            this.model = new LoginModel();
            this.authService = new AuthService();
        }

        public string Login
        {
            get { return this.model.Login; }
            set
            {
                this.model.Login = value;
                this.OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return this.model.Password; }
            set
            {
                this.model.Password = value;
                this.OnPropertyChanged();
            }
        }

        private async void SignIn()
        {
            var operationStatus = await this.ExecuteOperation(async () => await authService.AuthAsync(this.model));
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