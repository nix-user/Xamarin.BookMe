using System;
using Android.Locations;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Pages.MyBookPages;
using Java.Util;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : ContentPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string BodyIntervalIsInvalid = "Нет подключения к интернету";
        private const string Ok = "Ok";

        public static User CurrentUser { get; set; }

        public SelectPage(User currentUser)
        {
            this.InitializeComponent();
            this.Date.MinimumDate = DateTime.Now;
            this.TimeTo.Time = DateTime.Now.TimeOfDay;
            this.TimeFrom.Time = DateTime.Now.TimeOfDay;
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

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            if (TimeFrom.Time < TimeTo.Time)
            {
                Room room = new Room() { IsBig = IsBig.IsToggled, IsHasPolykom = IsPolinom.IsToggled };
                ReservationModel reservation = new ReservationModel()
                {
                    Date = Date.Date,
                    Room = room,
                    From = TimeFrom.Time,
                    To = TimeTo.Time,
                    Author = CurrentUser,
                    IsRecursive = IsRecursive.IsToggled
                };
                ErrorInterval.Text = string.Empty;
                if (!await RestURl.IsConnected())
                {
                    await this.DisplayAlert(HeadError, BodyInternetIsNotExist, Ok);
                    return;
                }

                await this.Navigation.PushAsync(new MainPage(CurrentUser, new ListRoomPage(reservation, CurrentUser)));
            }
            else
            {
                await this.DisplayAlert(HeadError, BodyIntervalIsInvalid, Ok);
            }
        }

        public async void MyReservations_OnClicked(object sender, EventArgs e)
        {
            if (!await RestURl.IsConnected())
            {
                await this.DisplayAlert(HeadError, BodyInternetIsNotExist, Ok);
                return;
            }

           await this.Navigation.PushAsync(new MainPage(CurrentUser, new TabPanelPage(CurrentUser)));
        }
    }
}