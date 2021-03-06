﻿using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.Enums;
using BookMeMobile.Infrastructure.Abstract;
using BookMeMobile.Model;
using BookMeMobile.Resources;
using BookMeMobile.ViewModels.Abstract;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    internal class AddReservationViewModel : BaseViewModel
    {
        private ListRoomManager service;
        private AddReservationModel model;

        public AddReservationViewModel(
            SelectModel filterParameter,
            RoomViewModel roomModel,
            ListRoomManager roomManager,
            INavigationService navigationService) : base(navigationService)
        {
            this.model = new AddReservationModel(filterParameter, roomModel);
            this.service = roomManager;
            this.AddReservationCommand = new Command(this.AddReservation);
            this.GoBackCommand = new Command(this.GoBack);
        }

        private async void GoBack()
        {
            await this.NavigationService.XamarinNavigation.PopAsync();
        }

        public ICommand AddReservationCommand { get; protected set; }

        public ICommand GoBackCommand { get; protected set; }

        public string Title
        {
            get { return this.model.Title; }
            set { this.model.Title = value; }
        }

        public string NumberRoom
        {
            get { return this.model.NumberRoom; }
            set { this.model.NumberRoom = value; }
        }

        public string Date
        {
            get { return this.model.Date.ToString("dd.MM.yy"); }
        }

        public string From
        {
            get { return this.model.From.TimeOfDay.ToString(@"hh\:mm"); }
        }

        public string To
        {
            get { return this.model.To.TimeOfDay.ToString(@"hh\:mm"); }
        }

        public bool IsLarge
        {
            get { return this.model.IsLarge; }
        }

        public bool HasPolycom
        {
            get { return this.model.HasPolycom; }
        }

        public async void AddReservation()
        {
            if (!string.IsNullOrEmpty(this.Title))
            {
                var operationResult =
                    (await this.ExecuteOperation(async () => await this.service.AddReservation(this.model)))
                        .Status;
                if (operationResult == StatusCode.Ok)
                {
                    await this.ShowInformationDialog(AlertMessages.SuccessHeader, AlertMessages.SuccessBody);
                    App.Current.MainPage = this.NavigationService.ShowViewModelAsMainPageWithMenu<SelectViewModel>();
                }
                else
                {
                    await this.ShowErrorMessage(operationResult);
                }
            }
            else
            {
                await this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.FieldIsEmpty);
            }
        }
    }
}