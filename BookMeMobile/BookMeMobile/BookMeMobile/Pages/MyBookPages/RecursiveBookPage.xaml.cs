﻿using System;
using System.Collections.Generic;
using System.Linq;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class RecursiveReservationPage : ContentPage
    {
        private readonly string reservationingHeadChecking = "Подтвердите действие";
        private readonly string reservationIsDelete = "Снять бронирование бронирование?";
        private readonly string reservationingBodySucces = "Комната успешно разбронирована";
        private readonly string reservationingHeadSuccess = "Действие успешно выполнено";
        private readonly string reservationButonOK = "Да";
        private readonly string reservationButonNO = "Нет";

        public List<MyReservationViewResult> ResultRoom { get; set; }

        private ListRoomManager manager;

        public User CurrentUser { get; set; }

        public RecursiveReservationPage(User user)
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager(user);
            this.ResultRoom = this.manager.GetUserReservationingsRecursive();
            if (this.ResultRoom.Any())
            {
                this.listRoom.BindingContext = this.ResultRoom;
            }
            else
            {
                messageIsEmpty.IsVisible = true;
            }
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idBook = int.Parse(((Button)sender).ClassId);
            bool b = await DisplayAlert(this.reservationingHeadChecking, this.reservationIsDelete, this.reservationButonOK, this.reservationButonNO);
            if (b)
            {
                await this.manager.DeleteReservationRecursive(idBook);
                await this.DisplayAlert(this.reservationingHeadSuccess, this.reservationingBodySucces, this.reservationButonOK);
                await this.Navigation.PopModalAsync();
            }
        }
    }
}