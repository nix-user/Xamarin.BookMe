using System.Windows.Input;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model.Login;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IAuthService authService;
        private LoginModel model;

        public ICommand SignInCommand { get; private set; }

        public LoginViewModel(IAuthService authService, INavigationService navigationService) : base(navigationService)
        {
            this.authService = authService;
            this.SignInCommand = new Command(this.SignIn);
            this.model = new LoginModel();
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
            var operationStatus = await this.ExecuteOperation(async () => await this.authService.AuthAsync(this.model));
            if (operationStatus == StatusCode.Ok)
            {
                await this.NavigationService.XamarinNavigation.PushAsync(new MainPage(new SelectPage()));

                //TODO: uncomment when SelectPage will be updated to custom mvvm navigation logic
                //this.NavigationService.ShowViewModel(new LoginViewModel(this.authService, this.NavigationService));
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