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
        private readonly string BookingHeadChecking = "Подтвердите действие";
        private readonly string BookingBodySucces = "Комната успешно забронирована";
        private readonly string BookingHeadSuccess = "Действие успешно выполнено";
        private readonly string BookButonOK = "Да";
        private readonly string BookButonNO = "Нет";


        public List<MyBookViewResult> ResultRoom { get; set; }
        private ListRoomManager list;
        public ListRoomPage(Booking book,User currentUser)
        {
            InitializeComponent();
            this.list = new ListRoomManager(book,currentUser);
            ResultRoom = list.AddUserBookInRange(book);
            ResultRoom.AddRange(list.AddUserBookPartRange(book));
            ResultRoom.AddRange(list.Search(book));
            if (!ResultRoom.Any())
            {
                isRoom.IsVisible = true;
            }
            listUserRoomInRange.BindingContext = ResultRoom;
        }


        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idRoom = int.Parse(((Button)sender).ClassId);
            string bookBody = list.Booking(idRoom);
            bool b = await DisplayAlert(BookingHeadChecking, bookBody, BookButonOK, BookButonNO);
            if (b)
            {
                
                list.AddBook(idRoom);
                await DisplayAlert(BookingHeadSuccess, BookingBodySucces, BookButonOK);
                await Navigation.PopAsync();
            }
            
        }
    }
}
