using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Resources;

namespace BookMeMobile.ViewModels.Abstract
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<StatusCode, string> errorMessagesDictionary = new Dictionary<StatusCode, string>()
        {
            { StatusCode.Error, AlertMessages.ServerError },
            { StatusCode.ConnectionProblem, AlertMessages.NoInternetConnection }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public Func<string, string, string, Task> ShowInfoMessage { get; set; }

        public Action<bool> ToggleProgressIndicator { get; set; }

        protected INavigationService NavigationService { get; set; }

        protected BaseViewModel(INavigationService navigationService)
        {
            this.NavigationService = navigationService;
        }

        public virtual void OnAttachedToView()
        {
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected async Task ShowInformationDialog(string header, string content)
        {
            await this.ShowInfoMessage?.Invoke(header, content, AlertMessages.InfoAlertCanelText);
        }

        protected async Task ShowErrorMessage(StatusCode statusCode)
        {
            await this.ShowInformationDialog(AlertMessages.ErrorHeader, this.errorMessagesDictionary[statusCode]);
        }

        protected async Task<TReturn> ExecuteOperation<TReturn>(Func<Task<TReturn>> operation)
        {
            this.ToggleProgressIndicator?.Invoke(true);
            var result = await operation();
            this.ToggleProgressIndicator?.Invoke(false);
            return result;
        }
    }
}