using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using Android.Text.Style;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Java.Lang;
using Javax.Security.Auth;
using Xamarin.Forms;
using ZXing.Net.Mobile.Forms;
using Result = ZXing.Result;

namespace BookMeMobile.Pages
{
    public partial class QrReservation : ContentPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string BodyErrorInternetOrServer = "Ошибка с сервером либо с интернетом";
        private const string BodyIntervalIsInvalid = "Ввведен неверный интервал";
        private const string BodyError = "Ошибка на сервере";
        private const string RoomIsBook = "Комната занята";

        private const string Ok = "Ok";
        private ListRoomManager manager;

        public async void ScanResult(Result result)
        {
            this.manager = new ListRoomManager();
            var reservation = await this.manager.GetRoomCurrentReservations(result.Text);
            switch (reservation.Status)
            {
                case StatusCode.Ok:
                    {
                        this.Sucess(reservation.Result, result);
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

        private async void Sucess(IEnumerable<ReservationModel> reservation, Result result)
        {
            if (reservation == null || !reservation.Any())
            {
                string body = string.Format("Комната: {0} \n От: {1:hh\\:mm} \n До: {2:hh\\:mm}", result.Text, DateTime.Now, DateTime.Now.AddHours(1));
                bool saveOrNot = await DisplayAlert("Забронировать комнату?", body, "Да", "Нет");
                if (saveOrNot)
                {
                    StatusCode savStatusCode = (await this.manager.AddReservationInHour(result.Text)).Status;
                    if (savStatusCode == StatusCode.Ok)
                    {
                        await this.DisplayAlert("Успешно", "Комната успешно занята", Ok);
                    }
                    else
                    {
                        await this.DisplayAlert(HeadError, BodyErrorInternetOrServer, Ok);
                    }
                }
            }
            else
            {
                await this.DisplayAlert(HeadError, RoomIsBook, Ok);
            }
        }
    }
}