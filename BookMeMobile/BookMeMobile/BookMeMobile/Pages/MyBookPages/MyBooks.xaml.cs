using System;
using System.Collections.Generic;
using System.Linq;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class MyReservation : ContentPage
    {
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private readonly string bookingHeadChecking = "Подтвердите действие";
        private readonly string bookIsDelete = "Снять бронирование бронирование?";
        private readonly string bookingBodySucces = "Комната успешно разбронирована";
        private readonly string bookingHeadSuccess = "Действие успешно выполнено";
        private readonly string bookingHeadError = "Ошибка";
        private readonly string bookingBodyError = "Действие не было выполнено";
        private readonly string bookButonOK = "Да";
        private readonly string bookButonNO = "Нет";

        public List<MyReservationViewResult> ResultRoom { get; set; }

        private ListRoomManager manager;

        public User CurrentUser { get; set; }

        public MyReservation(User user)
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager(user);
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idBook = int.Parse(((Button)sender).ClassId);
            bool b = await DisplayAlert(this.bookingHeadChecking, this.bookIsDelete, this.bookButonOK, this.bookButonNO);
            if (b)
            {
                this.ShowMessage(await this.manager.DeleteReservation(idBook));
            }
        }

        private async void ShowMessage(StatusCode status)
        {
            if (status == StatusCode.Ok)
            {
                await this.DisplayAlert(this.bookingHeadSuccess, this.bookingBodySucces, this.bookButonOK);
                await this.Navigation.PopModalAsync();
            }

            if (status == StatusCode.Error)
            {
                await this.DisplayAlert(this.bookingHeadError, this.bookingBodyError, this.bookButonOK);
                await this.Navigation.PopModalAsync();
            }

            if (status == StatusCode.NoInternet)
            {
                await this.DisplayAlert(this.bookingHeadError, BodyInternetIsNotExist, this.bookButonOK);
                await this.Navigation.PopModalAsync();
            }
        }
    }
}