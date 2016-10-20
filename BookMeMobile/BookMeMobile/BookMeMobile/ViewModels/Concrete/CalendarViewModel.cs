using System;
using System.Windows.Input;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.ViewModels.Abstract;
using BookMeMobile.ViewModels.Concrete;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels
{
    public class CalendarViewModel : BaseViewModel
    {
        private SelectModel model;

        public CalendarViewModel(SelectModel selectModel, INavigationService navigationService) : base(navigationService)
        {
            this.SelectDateCommand = new Command(this.SelectDate);
            this.ButtonOkCommand = new Command(this.ButtonOk);
            this.GoBackCommand = new Command(this.GoBack);
            this.model = selectModel;
        }

        public DateTime SelectedDate
        {
            get { return this.model.Date; }

            set { this.model.Date = value; }
        }

        public ICommand SelectDateCommand { get; set; }

        public ICommand ButtonOkCommand { get; set; }

        public ICommand GoBackCommand { get; set; }

        private void SelectDate(object value)
        {
            DateTime selectDate = (DateTime)value;
            this.SelectedDate = selectDate;
        }

        private void ButtonOk()
        {
            App.Current.MainPage = this.NavigationService.ShowViewModelAsMainPageWithMenu<SelectViewModel>(new { model = this.model });
        }

        private void GoBack()
        {
            this.NavigationService.XamarinNavigation.PopAsync();
        }
    }
}