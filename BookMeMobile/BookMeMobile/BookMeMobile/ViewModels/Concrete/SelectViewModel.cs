using System;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.BL.Abstract;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages.MyReservations;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
using Microsoft.Practices.Unity;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class SelectViewModel : BaseViewModel
    {
        private SelectModel model;
        private ListRoomManager service;
        private IReservationService reservationServise;

        public SelectViewModel(ListRoomManager listRoomManager, IReservationService reservationServise, INavigationService navigationService, SelectModel model) : base(navigationService)
        {
            this.model = model;
            this.service = listRoomManager;
            this.reservationServise = reservationServise;
            this.GoToMyReservation = new Command(this.GetMyReservation);
            this.GoToSearch = new Command(this.Search);
            this.GoToCalendarCommand = new Command(this.GoToCalendar);
        }

        public ICommand GoToMyReservation { get; protected set; }

        public ICommand GoToSearch { get; protected set; }

        public ICommand GoToCalendarCommand { get; set; }

        public void GoToCalendar()
        {
            this.NavigationService.ShowViewModel<CalendarViewModel>();
        }

        private async void Search()
        {
            if (this.model.From.TimeOfDay < this.model.To.TimeOfDay)
            {
                this.ValidInterval();
            }
            else
            {
                await this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.WrongIntervalTime);
            }
        }

        private async void ValidInterval()
        {
            if (this.model.Date.Date != DateTime.Now.Date || this.model.From.TimeOfDay >= DateTime.Now.TimeOfDay)
            {
                var operationResult =
                                  (await this.ExecuteOperation(async () => await this.service.GetEmptyRoom(this.model)));

                if (operationResult.Status == StatusCode.Ok)
                {
                    this.NavigationService.ShowViewModel<ListRoomViewModel>(new { rooms = operationResult.Result, selectModel = this.model });
                }
                else
                {
                    await this.ShowErrorMessage(operationResult.Status);
                }
            }
            else
            {
                await this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.WrongIntervalInThePast);
            }
        }

        private async void GetMyReservation()
        {
            var operationResult =
                                  (await this.ExecuteOperation(async () => await this.reservationServise.GetUserReservations()));

            if (operationResult.Status == StatusCode.Ok)
            {
                await this.NavigationService.XamarinNavigation.PushAsync(new MyReservationsPage(operationResult.Result));
            }
            else
            {
                await this.ShowErrorMessage(operationResult.Status);
            }
        }

        public DateTime Date
        {
            get { return this.model.Date; }
            set { this.model.Date = value; }
        }

        public DateTime From
        {
            get { return this.model.From; }
            set
            {
                var currentTime = this.model.RoundTime(this.SetValidDate(value));
                if (currentTime >= this.To)
                {
                    this.To = this.model.RoundTime(currentTime.AddMinutes(30));
                }

                this.model.From = currentTime;
            }
        }

        public DateTime To
        {
            get { return this.model.To; }
            set
            {
                this.model.To = this.model.RoundTime(this.SetValidDate(value));
                this.OnPropertyChanged();
            }
        }

        public bool IsLarge
        {
            get { return this.model.IsLarge; }
            set { this.model.IsLarge = value; }
        }

        public bool HasPolycom
        {
            get { return this.model.HasPolycom; }
            set { this.model.HasPolycom = value; }
        }

        private DateTime SetValidDate(DateTime invalidDate)
        {
            DateTime curretTime = this.model.Date;
            return new DateTime(curretTime.Year, curretTime.Month, curretTime.Day, invalidDate.Hour, invalidDate.Minute, invalidDate.Second);
        }
    }
}