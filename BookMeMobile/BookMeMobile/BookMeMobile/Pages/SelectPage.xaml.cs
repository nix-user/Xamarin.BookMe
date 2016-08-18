using System;
using BookMeMobile.Entity;
using Java.Util;
using Xamarin.Forms;

namespace BookMeMobile.Pages
{
    public partial class SelectPage : ContentPage
    {
        public User CurrentUser { get; set; }

        public SelectPage(User currentUser)
        {
            this.InitializeComponent();
            this.Date.MinimumDate = DateTime.Now;
            this.CurrentUser = currentUser;
        }

        private void DatePickerDateSelected(object sender, DateChangedEventArgs e)
        {
            if (this.LabelDate != null)
            {
                LabelDate.Text = "Вы выбрали " + e.NewDate.ToString("dd/MM/yyyy");
            }
        }

        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (TimeFrom.Time < TimeTo.Time)
            {
                Room room = new Room() { IsBig = IsBig.IsToggled, IsHasPolykom = IsPolinom.IsToggled };
                Booking booking = new Booking()
                {
                    Date = Date.Date, 
                    Room = room, 
                    From = TimeFrom.Time, 
                    To = TimeTo.Time, 
                    WhoBook = this.CurrentUser
                };
                ErrorInterval.Text = string.Empty;
                Navigation.PushAsync(new ListRoomPage(booking, this.CurrentUser));
            }
            else
            {
                ErrorInterval.Text = "Неверный интервал";
            }
        }

        private void MyBook_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyBooks(this.CurrentUser));
        }
    }
}
