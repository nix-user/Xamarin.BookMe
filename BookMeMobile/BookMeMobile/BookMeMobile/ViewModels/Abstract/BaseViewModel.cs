using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Enums;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels
{
    internal abstract class BaseViewModel : INotifyPropertyChanged
    {
        private readonly Dictionary<StatusCode, string> errorMessagesDictionary = new Dictionary<StatusCode, string>()
        {
            { StatusCode.Error, AlertMessages.ServerError },
            { StatusCode.NoInternet, AlertMessages.NoInternetConnection }
        };

        public event PropertyChangedEventHandler PropertyChanged;

        public Action<string, string> ShowInfoMessage { get; set; }

        protected INavigation Navigation { get; set; }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void ShowMessage(string header, string content)
        {
            this.ShowInfoMessage?.Invoke(header, content);
        }

        protected void ShowErrorMessage(StatusCode statusCode)
        {
            this.ShowMessage(AlertMessages.ErrorHeader, this.errorMessagesDictionary[statusCode]);
        }
    }
}