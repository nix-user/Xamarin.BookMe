using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private readonly string reservationingHeadChecking = "Подтвердите действие";
        private readonly string reservationingBodySucces = "Комната успешно забронирована";
        private readonly string reservationingHeadSuccess = "Действие успешно выполнено";
        private readonly string reservationButonOK = "Да";
        private readonly string reservationButonNO = "Нет";

        public List<MyReservationViewResult> ResultRoom { get; set; }

        private ReservationModel currentBooking;

        private ListRoomManager list;

        public ListRoomPage(ReservationModel reservation, User currentUser)
        {
            this.InitializeComponent();
            this.currentBooking = reservation;
            this.list = new ListRoomManager(reservation, currentUser);
            this.ResultRoom = this.list.AddUserReservationInRange(reservation);
            this.ResultRoom.AddRange(this.list.AddUserReservationPartRange(reservation));
            this.ResultRoom.AddRange(this.list.Search(reservation));
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
            bool b = await this.DisplayAlert(this.reservationingHeadChecking, reservationBody, this.reservationButonOK, this.reservationButonNO);
            if (b)
            {
                if (!this.currentBooking.IsRecursive)
                {
                    this.list.AddReservation(idRoom);
                }
                else
                {
                    this.list.AddRecursiveReservation(idRoom);
                }

                await this.DisplayAlert(this.reservationingHeadSuccess, this.reservationingBodySucces, this.reservationButonOK);
                await Navigation.PopAsync();
            }
        }
    }
}