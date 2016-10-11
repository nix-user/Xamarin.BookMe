using System;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Pages.MyReservations;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    public class SelectViewModel : BaseViewModel
    {
        private SelectModel model;
        private ListRoomManager service;

        public SelectViewModel(ListRoomManager listRoomManager, INavigationService navigationService) : base(navigationService)
        {
            this.model = new SelectModel();
            this.service = listRoomManager;

            this.GoToMyReservation = new Command(this.GetMyReservation);
            this.GoToSearch = new Command(this.Search);
        }

        public ICommand GoToMyReservation { get; protected set; }

        public ICommand GoToSearch { get; protected set; }

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
            if (this.Date.Date != DateTime.Now.Date || this.model.From.TimeOfDay >= DateTime.Now.TimeOfDay)
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
            await this.NavigationService.XamarinNavigation.PushAsync(new MyReservationsPage());
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
            return new DateTime(this.Date.Year, this.Date.Month, this.Date.Day, invalidDate.Hour, invalidDate.Minute, invalidDate.Second);
        }
    }
}