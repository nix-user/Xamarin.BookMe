using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages;
using BookMeMobile.Resources;
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    internal class AddReservationViewModel : BaseViewModel
    {
        private ListRoomManager service;
        private AddReservationModel model;

        public AddReservationViewModel(SelectModel filterParametr, int idRoom)
        {
            this.model = new AddReservationModel(filterParametr, idRoom);
            this.service = new ListRoomManager();
            this.AddReservationCommand = new Command(this.AddReservation);
            this.GoBackCommand = new Command(this.GoBack);
        }

        private async void GoBack()
        {
          await this.Navigation.PopModalAsync();
        }

        public ICommand AddReservationCommand { get; protected set; }

        public ICommand GoBackCommand { get; protected set; }

        public string Title
        {
            get { return this.model.Title; }
            set { this.model.Title = value; }
        }

        public string Date
        {
            get { return this.model.From.ToString("d"); }
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
                    this.ShowInformationDialog(AlertMessages.SuccessHeader, AlertMessages.SuccessBody);
                    await this.Navigation.PopModalAsync();
                }

                this.ShowErrorMessage(operationResult);
            }
            else
            {
                this.ShowInformationDialog(AlertMessages.ErrorHeader, AlertMessages.FieldIsEmpty);
            }
        }
    }
}