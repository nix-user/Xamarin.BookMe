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
using Xamarin.Forms;

namespace BookMeMobile.ViewModels.Concrete
{
    internal class AddReservationViewModel : BaseViewModel
    {
        private readonly string reservationingHeadChecking = "Подтвердите действие";
        private readonly string reservationingBodySucces = "Комната успешно забронирована";
        private readonly string reservationingHeadSuccess = "Действие успешно выполнено";
        private readonly string reservationingHeadError = "Ошибка";
        private readonly string reservationingBodyError = "Ошибка на сервере";
        private readonly string errorHeadEmptyTitle = "Заполните поле назначение";
        private readonly string reservationingBodyNoInternet = "Нет доступа к интернету";
        private readonly string reservationButonOK = "Да";
        private readonly string reservationButonNO = "Нет";
        private ListRoomManager manager;
        private RoomFilterParameters parametr;
        private Reservation reservation;

        public AddReservationViewModel(RoomFilterParameters parametr, int idRoom)
        {
            this.parametr = parametr;
            this.reservation = new Reservation()
            {
                From = parametr.From,
                To = parametr.To,
                ResourceId = idRoom,
                Duration = parametr.To - parametr.From,
                IsRecursive = false
            };
            this.manager = new ListRoomManager();
            this.AddReservationCommand = new Command(this.AddReservation);
        }

        public ICommand AddReservationCommand { get; protected set; }

        public ICommand GoBackCommand { get; protected set; }

        public string Title
        {
            get { return this.reservation.Title; }
            set { this.reservation.Title = value; }
        }

        public string Date
        {
            get { return this.parametr.From.Date.ToString("d"); }
        }

        public string From
        {
            get { return this.parametr.From.TimeOfDay.ToString(@"hh\:mm"); }
        }

        public string To
        {
            get { return this.parametr.To.TimeOfDay.ToString(@"hh\:mm"); }
        }

        public string IsLarge
        {
            get { return string.Format("{0:Да;0;Нет}", this.parametr.IsLarge.GetHashCode()); }
        }

        public string HasPolycom
        {
            get { return string.Format("{0:Да;0;Нет}", this.parametr.HasPolycom.GetHashCode()); }
        }

        public async void AddReservation(object someObject)
        {
            if (this.Title != null)
            {
                StatusCode statusCodeOperation = StatusCode.Error;
                statusCodeOperation = (await this.manager.AddReservation(this.reservation)).Status;
            }
        }
    }
}