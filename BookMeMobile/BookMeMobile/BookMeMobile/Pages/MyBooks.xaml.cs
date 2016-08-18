using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.BL;
using BookMeMobile.Entity;
using Java.Nio.Channels;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class MyBooks : ContentPage
    {
        private readonly string BookingHeadChecking = "Подтвердите действие";
        private readonly string BookIsDelete = "Снять бронирование бронирование?";
        private readonly string BookingBodySucces = "Комната успешно разбронирована";
        private readonly string BookingHeadSuccess = "Действие успешно выполнено";
        private readonly string BookButonOK = "Да";
        private readonly string BookButonNO = "Нет";
        public List<MyBookViewResult> ResultRoom { get; set; }
        private ListRoomManager manager;
        public User CurrentUser { get; set; }
        public MyBooks(User user)
        {
            InitializeComponent();
            manager=new ListRoomManager(user);
            ResultRoom = manager.GetUserBookings();
            if (ResultRoom.Any())
            {
                listRoom.BindingContext = ResultRoom;
            }
            else
            {
                listRoom.Header = ListRoomIsEmpty();
            }
        }

        public Label ListRoomIsEmpty()
        {
            return new Label()
            {
                Text = "Комнат нет",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                FontSize = 24
            };
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            int idBook = int.Parse(((Button)sender).ClassId);
            bool b = await DisplayAlert(BookingHeadChecking, BookIsDelete, BookButonOK, BookButonNO);
            if (b)
            {
                manager.DeleteBook(idBook);
                await DisplayAlert(BookingHeadSuccess, BookingBodySucces, BookButonOK);
                await Navigation.PopAsync();
            }
        }
    }
}
