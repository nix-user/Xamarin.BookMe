using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private readonly string bookingHeadChecking = "Подтвердите действие";
        private readonly string bookingBodySucces = "Комната успешно забронирована";
        private readonly string bookingHeadSuccess = "Действие успешно выполнено";
        private readonly string bookButonOK = "Да";
        private readonly string bookButonNO = "Нет";

        public List<MyBookViewResult> ResultRoom { get; set; }

        private ListRoomManager list;

        public ListRoomPage(Booking book, User currentUser)
        {
            this.InitializeComponent();
            this.list = new ListRoomManager(book, currentUser);
            this.ResultRoom = this.list.AddUserBookInRange(book);
            this.ResultRoom.AddRange(this.list.AddUserBookPartRange(book));
            this.ResultRoom.AddRange(this.list.Search(book));
            if (!this.ResultRoom.Any())
            {
                isRoom.IsVisible = true;
            }

            listUserRoomInRange.BindingContext = this.ResultRoom;
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idRoom = int.Parse(((Button)sender).ClassId);
            string bookBody = this.list.Booking(idRoom);
            bool b = await this.DisplayAlert(this.bookingHeadChecking, bookBody, this.bookButonOK, this.bookButonNO);
            if (b)
            {
                this.list.AddBook(idRoom);
                await this.DisplayAlert(this.bookingHeadSuccess, this.bookingBodySucces, this.bookButonOK);
                await Navigation.PopAsync();
            }
        }
    }
}