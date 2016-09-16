﻿using System;
using System.Collections.Generic;
using Android.Locations;
using BookMeMobile.BL;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.Pages.MyBookPages;
using Java.Util;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : ContentPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string BodyIntervalIsInvalid = "Ввведен неверный интервал";
        private const string BodyError = "Ошибка на сервере";
        private const string Ok = "Ok";

        public static User CurrentUser { get; set; }

        private ListRoomManager manager;

        public SelectPage()
        {
            this.InitializeComponent();
            this.Date.MinimumDate = DateTime.Now;
            this.TimeTo.Time = DateTime.Now.TimeOfDay;
            this.TimeFrom.Time = DateTime.Now.TimeOfDay;
            this.manager = new ListRoomManager();
            this.SettingPaddingForWinPhone();
        }

        private void SettingPaddingForWinPhone()
        {
            if (Device.OS == TargetPlatform.Windows || Device.OS == TargetPlatform.WinPhone)
            {
                this.checkerLayout.Padding = new Thickness(0, 0, 0, 0);
                this.timeLayout.Padding = new Thickness(0, 0, 0, 0);
            }
        }

        private void DatePickerDateSelected(object sender, DateChangedEventArgs e)
        {
            if (this.LabelDate != null)
            {
                LabelDate.Text = "Вы выбрали " + e.NewDate.ToString("dd/MM/yyyy");
            }
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (TimeFrom.Time < TimeTo.Time)
            {
                Room room = new Room() { IsBig = IsBig.IsToggled, IsHasPolykom = IsPolinom.IsToggled };
                RoomFilterParameters reservation = new RoomFilterParameters()
                {
                    From = Date.Date + TimeFrom.Time,
                    To = Date.Date + TimeTo.Time,
                    HasPolycom = IsPolinom.IsToggled,
                    IsLarge = IsBig.IsToggled
                };

                var searchList = await this.manager.GetEmptyRoom(reservation);
                switch (searchList.StatusCode)
                {
                    case StatusCode.Ok:
                        {
                            await this.Navigation.PushAsync(new MainPage(new ListRoomPage(searchList.Result)));
                            break;
                        }

                    case StatusCode.NoInternet:
                        {
                            await this.DisplayAlert(HeadError, BodyInternetIsNotExist, Ok);
                            break;
                        }

                    case StatusCode.Error:
                        {
                            await this.DisplayAlert(HeadError, BodyError, Ok);
                            break;
                        }
                }
            }
            else
            {
                await this.DisplayAlert(HeadError, BodyIntervalIsInvalid, Ok);
            }
        }

        public async void MyReservations_OnClicked(object sender, EventArgs e)
        {
            var allReservatioons = await this.manager.GetAllUserReservation();
            switch (allReservatioons.StatusCode)
            {
                case StatusCode.Ok:
                    {
                        await this.Navigation.PushAsync(
                            new TabPanelPage(CurrentUser, allReservatioons.Result));
                        break;
                    }

                case StatusCode.NoInternet:
                    {
                        await this.DisplayAlert(HeadError, BodyInternetIsNotExist, Ok);
                        break;
                    }

                case StatusCode.Error:
                    {
                        await this.DisplayAlert(HeadError, BodyError, Ok);
                        break;
                    }

                case StatusCode.NoAuthorize:
                    {
                        await this.Navigation.PushAsync(new LoginPage());
                        break;
                    }
            }
        }
    }
}