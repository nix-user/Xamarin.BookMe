using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Android.Locations;
using BookMeMobile.BL;
using BookMeMobile.Data;
using BookMeMobile.Entity;
using BookMeMobile.Enums;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using BookMeMobile.Pages.Login;
using BookMeMobile.Pages.MyBookPages;
using BookMeMobile.Render;
using Java.Util;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : ActivityIndicatorPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyInternetIsNotExist = "Проверьте подключение к интернету и повторите попытку";
        private const string BodyIntervalIsInvalid = "Ввведен неверный интервал";
        private const string BodyError = "Внутренняя ошибка сервера";
        private const string Ok = "Ok";

        public static User CurrentUser { get; set; }

        private ListRoomManager manager;

        public SelectPage()
        {
            this.InitializeComponent();
            this.manager = new ListRoomManager();
            this.SettingPaddingForWinPhone();
            this.TimeTo.SetValue(TimePicker.TimeProperty, this.TimeFrom.Time.Add(TimeSpan.FromHours(1)));

            this.SetUpActivityIndicator(this.loader, this.rootLayout);
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
                RoomFilterParameters reservation = new RoomFilterParameters()
                {
                    From = Date.Date + TimeFrom.Time,
                    To = Date.Date + TimeTo.Time,
                    HasPolycom = IsPolinom.IsToggled,
                    IsLarge = IsBig.IsToggled
                };

                BaseOperationResult<IEnumerable<Room>> searchListRetrieval = null;
                await this.PerformWithActivityIndicator(async () =>
                {
                    searchListRetrieval = await this.manager.GetEmptyRoom(reservation);
                });

                switch (searchListRetrieval.Status)
                {
                    case StatusCode.Ok:
                        {
                            await this.Navigation.PushAsync(new MainPage(new ListRoomPage(searchListRetrieval.Result, reservation)));
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
            else
            {
                await this.DisplayAlert(HeadError, BodyIntervalIsInvalid, Ok);
            }
        }

        public async void MyReservations_OnClicked(object sender, EventArgs e)
        {
            var allReservatioons = await this.manager.GetAllUserReservation();
            switch (allReservatioons.Status)
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