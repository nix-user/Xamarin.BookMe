﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages.MyBookPages
{
    public partial class AllMyBook : ContentPage
    {
        private readonly string bookingHeadChecking = "Подтвердите действие";
        private readonly string bookIsDelete = "Снять бронирование бронирование?";
        private readonly string bookingBodySucces = "Комната успешно разбронирована";
        private readonly string bookingHeadSuccess = "Действие успешно выполнено";
        private readonly string bookButonOK = "Да";
        private readonly string bookButonNO = "Нет";

        public List<MyBookViewResult> ResultRoom { get; set; }

        private ListRoomManager manager;

        public User CurrentUser { get; set; }

        public AllMyBook(User user)
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager(user);
            this.ResultRoom = this.manager.GetUserBookingsRecursive();
            this.ResultRoom.AddRange(this.manager.GetUserBookings());
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
            bool b = await DisplayAlert(this.bookingHeadChecking, this.bookIsDelete, this.bookButonOK, this.bookButonNO);
            if (b)
            {
                this.manager.DeleteBookRecursive(idBook);
                await this.DisplayAlert(this.bookingHeadSuccess, this.bookingBodySucces, this.bookButonOK);
                await this.Navigation.PopModalAsync();
            }
        }
    }
}