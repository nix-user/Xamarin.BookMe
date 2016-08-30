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

namespace BookMeMobile.Pages
{
    public partial class QrBook : ContentPage
    {
        private static ZXingScannerPage scanPage;
        private BookingRepository bookingRepository;
        private RoomRepository roomRepository;
        private ListRoomManager manager;
        private User curreUser;

        public QrBook() : base()
        {
            this.InitializeComponent();
            this.curreUser = SelectPage.CurrentUser;
            this.manager = new ListRoomManager(this.curreUser);
        }

        public async void GoCamera()
        {
            scanPage = new ZXingScannerPage();
            scanPage.OnScanResult += this.ScanResult;
            await Navigation.PushAsync(scanPage);
        }

        private async void ScanResult(Result result)
        {
            scanPage.IsScanning = false;
            await Navigation.PushAsync(new SelectPage(SelectPage.CurrentUser));
            Device.BeginInvokeOnMainThread(() =>
            {
                Booking attempt = manager.AttemptBook(result.Text, curreUser);
                DisplayAlert("Действие не может быть выполнено", "Комната занята", "Ok");
                //else
                //{
                //    Task<bool> saveOrNot = DisplayAlert("Подтверждение действия", "Комната" + attempt.Room + "От:" + attempt.From + "\n" + "До:" + attempt.To + "Занять комнату ?", "Да", "Нет");
                //    if (saveOrNot.Result)
                //    {
                //        DisplayAlert("Успешно", "Комната успешно занята", "Ok");
                //    }
                //}
            });
        }
    }
}