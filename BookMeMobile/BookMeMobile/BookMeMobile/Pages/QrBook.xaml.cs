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
    public partial class QrBook : ContentPage
    {
        public async void ScanResult(Result result, User currentUser)
        {
            ListRoomManager manager = new ListRoomManager(currentUser);
            Booking book = manager.AttemptBook(result.Text, currentUser);
            if (book != null)
            {
                bool saveOrNot = await DisplayAlert("Забронировать комнату?", string.Format("Комната: {0} \n От: {1:hh\\:mm} \n До: {2:hh\\:mm}", book.Room.Number, book.From, book.To), "Да", "Нет");
                if (saveOrNot)
                {
                    manager.AddBook(book);
                    await this.DisplayAlert("Успешно", "Комната успешно занята", "Ok");
                }
            }
            else
            {
              await this.DisplayAlert("Действие не может быть выполнено", "Комната занята", "Ok");
            }
        }
    }
}