﻿using System;
using Android.Locations;
using BookMeMobile.Entity;
using BookMeMobile.Pages.MyBookPages;
using Java.Util;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : ContentPage
    {
        public static User CurrentUser { get; set; }

        public SelectPage(User currentUser)
        {
            this.InitializeComponent();
            this.Date.MinimumDate = DateTime.Now;
            CurrentUser = currentUser;
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

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (TimeFrom.Time < TimeTo.Time)
            {
                Room room = new Room() { IsBig = IsBig.IsToggled, IsHasPolykom = IsPolinom.IsToggled };
                Booking booking = new Booking()
                {
                    Date = Date.Date,
                    Room = room,
                    From = TimeFrom.Time,
                    To = TimeTo.Time,
                    WhoBook = CurrentUser,
                    IsRecursive = IsRecursive.IsToggled
                };
                ErrorInterval.Text = string.Empty;
                Navigation.PushAsync(new ListRoomPage(booking, CurrentUser));
            }
            else
            {
                ErrorInterval.Text = "Неверный интервал";
            }
        }

        private void MyBook_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new TabPanelPage());
        }
    }
}