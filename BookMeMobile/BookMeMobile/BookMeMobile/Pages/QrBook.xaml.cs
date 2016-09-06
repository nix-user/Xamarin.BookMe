using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public async void ScanResult(Result result, User currentUser)
        {
            ListRoomManager manager = new ListRoomManager(currentUser);
            ReservationStatusModel reservation = manager.AttemptReservation(result.Text, currentUser);
            if (reservation.StatusCode == StatusCode.Ok)
            {
                if (reservation != null)
                {
                    string body = string.Format("Комната: {0} \n От: {1:hh\\:mm} \n До: {2:hh\\:mm}", reservation.Reservation.Room.Number, reservation.Reservation.From, reservation.Reservation.To);
                    bool saveOrNot = await DisplayAlert("Забронировать комнату?", body, "Да", "Нет");
                    if (saveOrNot)
                    {
                        await manager.AddReservation(reservation.Reservation);
                        await this.DisplayAlert("Успешно", "Комната успешно занята", "Ok");
                    }
                }
                else
                {
                    await this.DisplayAlert("Действие не может быть выполнено", "Комната занята", "Ok");
                }
            }
            else
            {
                await this.DisplayAlert("Ошибка", "Нет подключения к интернету", "Ok");
            }
        }
    }
}