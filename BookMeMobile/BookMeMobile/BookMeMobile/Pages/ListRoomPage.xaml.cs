using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private readonly string reservationingHeadChecking = "Подтвердите действие";
        private readonly string reservationingBodySucces = "Комната успешно забронирована";
        private readonly string reservationingHeadSuccess = "Действие успешно выполнено";
        private readonly string reservationingHeadError = "Ошибка";
        private readonly string reservationingBodyError = "Ошибка на сервере";
        private readonly string reservationingBodyNoInternet = "Нет доступа к интернету";
        private readonly string reservationButonOK = "Да";
        private readonly string reservationButonNO = "Нет";

        public List<Room> ResultRoom { get; set; }

        private ReservationModel currentBooking;

        private ListRoomManager list;

        public ListRoomPage(IEnumerable<Room> search)
        {
            this.InitializeComponent();
            this.list = new ListRoomManager();
            this.ResultRoom = search.ToList();
            if (!this.ResultRoom.Any())
            {
                isRoom.IsVisible = true;
            }

            listUserRoomInRange.BindingContext = this.ResultRoom;
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idRoom = int.Parse(((Button)sender).ClassId);
            string reservationBody = await this.list.ReservationMessag(idRoom);
            bool isBook = await this.DisplayAlert(this.reservationingHeadChecking, reservationBody, this.reservationButonOK, this.reservationButonNO);
            if (isBook)
            {
                if (!this.currentBooking.IsRecursive)
                {
                    this.AddNoRecursive(idRoom);
                }
                else
                {
                }

                await Navigation.PopAsync();
            }
        }

        private async void AddNoRecursive(int idRoom)
        {
            StatusCode statusCode = await this.list.AddReservation(idRoom);
            switch (statusCode)
            {
                case StatusCode.Ok:
                    {
                        await this.DisplayAlert(this.reservationingHeadSuccess, this.reservationingBodySucces, this.reservationButonOK);
                        break;
                    }

                case StatusCode.Error:
                    {
                        await this.DisplayAlert(this.reservationingHeadError, this.reservationingBodyError, this.reservationButonOK);
                        break;
                    }

                case StatusCode.NoInternet:
                    {
                        await this.DisplayAlert(this.reservationingHeadError, this.reservationingBodyNoInternet, this.reservationButonOK);
                        break;
                    }
            }
        }
    }
}