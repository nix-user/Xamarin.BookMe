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
        private CalendarModel model = new CalendarModel();

        public CalendarViewModel(DateTime? selectDate, INavigationService navigationService) : base(navigationService)
        {
            this.SelectDateCommand = new Command(this.SelectDate);
            this.ButtonOkCommand = new Command(this.ButtonOk);
            this.GoBackCommand = new Command(this.GoBack);
            this.SelectedDate = selectDate.Value.AddDays(1);
        }

        public DateTime MinDate => DateTime.Now.AddDays(-1);

        public DateTime StartDate => DateTime.Now;

        public DateTime? SelectedDate
        {
            get { return this.model.SelectedDate; }

            set { this.model.SelectedDate = value; }
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
            this.NavigationService.ShowViewModel<SelectViewModel>(new { date = this.SelectedDate });
        }

        private void GoBack()
        {
            this.NavigationService.ShowViewModel<SelectViewModel>();
        }
    }
}