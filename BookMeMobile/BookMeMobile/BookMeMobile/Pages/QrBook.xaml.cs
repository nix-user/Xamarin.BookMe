using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Text.Style;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Java.Lang;
using Xamarin.Forms;
using ZXing;
using ZXing.Net.Mobile.Forms;
using String = System.String;

namespace BookMeMobile.Pages
{
    public partial class QrReservation : ContentPage
    {
        private const string HeadError = "Ошибка";
        private const string BodyInternetIsNotExist = "Нет подключения к интернету";
        private const string BodyIntervalIsInvalid = "Ввведен неверный интервал";
        private const string BodyError = "Ошибка на сервере";
        private const string Ok = "Ok";

        public async void ScanResult(Result result, User currentUser)
        {
            ListRoomManager manager = new ListRoomManager(currentUser);
            var reservationRetrievalResult = await manager.GetRoomCurrentReservations(result.Text);
            switch (reservationRetrievalResult.Status)
            {
                case StatusCode.Ok:
                    {
                        if (reservationRetrievalResult.Result == null)
                        {
                            string body = string.Format("Комната: {0} \n От: {1:hh\\:mm} \n До: {2:hh\\:mm}", result.Text, DateTime.Now, DateTime.Now.AddHours(1));
                            bool saveOrNot = await DisplayAlert("Забронировать комнату?", body, "Да", "Нет");
                            if (saveOrNot)
                            {
                                StatusCode savStatusCode = await manager.AddReservationInHour(result.Text);
                                if (savStatusCode == StatusCode.Ok)
                                {
                                    await this.DisplayAlert("Успешно", "Комната успешно занята", "Ok");
                                }
                                else
                                {
                                    await this.DisplayAlert("Ошибка", "Ошибка с сервером либо с интернетом", "Ok");
                                }
                            }
                        }
                        else
                        {
                            await this.DisplayAlert("Действие не может быть выполнено", "Комната занята", "Ok");
                        }

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
    }
}