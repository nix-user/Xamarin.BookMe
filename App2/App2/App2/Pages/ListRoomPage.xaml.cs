using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.BL;
using App2.Entity;
using Xamarin.Forms;

namespace App2.Pages
{
    public partial class ListRoomPage : ContentPage
    {
        private readonly string BookingHead = "Подтвердите действие";
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
            bool b = await DisplayAlert(BookingHead, bookBody, BookButonOK, BookButonNO);
            if (b)
            {
                await Navigation.PopAsync();
                list.AddBook(idRoom);
            }
            
        }
    }
}
