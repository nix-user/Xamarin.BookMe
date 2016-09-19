﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private static readonly IEnumerable<string> UnallowedResources = new List<string>()
        {
            "709",
            "710",
            "712a",
            "713",
            "Netatmo",
            "NetatmoCityHall",
            "Projector 1",
            "Projector 2"
        };

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
        private ListRoomManager manager;
        private RoomFilterParameters currentReservation;

        public ListRoomPage(IEnumerable<Room> search, RoomFilterParameters reservation)
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager();
            this.ResultRoom = search.Where(x => !UnallowedResources.Contains(x.Number)).ToList();
            if (!this.ResultRoom.Any())
            {
                isRoom.IsVisible = true;
            }

            this.manager = new ListRoomManager();
            this.currentReservation = reservation;

            listUserRoomInRange.BindingContext = this.ResultRoom;
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idRoom = int.Parse(((Button)sender).ClassId);
            string reservationBody = await this.ReservationMessag(idRoom);
            bool isBook = await this.DisplayAlert(this.reservationingHeadChecking, reservationBody, this.reservationButonOK, this.reservationButonNO);
            if (isBook)
            {
               this.AddNoRecursive(idRoom);
               await Navigation.PopAsync();
            }
        }

        private async void AddNoRecursive(int idRoom)
        {
            StatusCode statusCode = (await this.manager.AddReservation(this.currentReservation, idRoom)).Status;
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

        public async Task<string> ReservationMessag(int idRoom)
        {
            Room currentRoom = this.ResultRoom.FirstOrDefault(x => x.Id == idRoom);
            return string.Format(
                " Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n Большая:{4} Поликом:{5}",
                this.currentReservation.From.Date.ToString("d"),
                this.currentReservation.From.TimeOfDay.ToString(@"hh\:mm"),
                this.currentReservation.To.TimeOfDay.ToString(@"hh\:mm"),
                currentRoom.Number,
                string.Format("{0:Да;0;Нет}", currentRoom.IsBig.GetHashCode()),
                string.Format("{0:Да;0;Нет}", currentRoom.IsHasPolykom.GetHashCode()));
        }
    }
}