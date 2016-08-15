using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Entity;
using App2.Pages;
using Xamarin.Forms;

namespace App2.Page
{
    public partial class SelectPage : ContentPage
    {
        public User CurrentUser { get; set; }
        public SelectPage(User currentUser)
        {
            InitializeComponent();
            Date.MinimumDate = DateTime.Now;
            this.CurrentUser = currentUser;
        }

        private void datePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            if(LabelDate != null){
                LabelDate.Text = "Вы выбрали " + e.NewDate.ToString("dd/MM/yyyy");
            }
        }
        
        private void Button_OnClicked(object sender, EventArgs e)
        {
            if (TimeFrom.Time < TimeTo.Time)
            {
                Room room = new Room() {IsBig = IsBig.IsToggled, IsHasPolynom = IsPolinom.IsToggled};
                Booking booking = new Booking()
                {
                    Date = Date.Date,
                    Room = room,
                    From = TimeFrom.Time,
                    To = TimeTo.Time,
                    WhoBook = this.CurrentUser
                };
                Navigation.PushAsync(new ListRoomPage(booking));
            }
            else
            {
                TimeFrom.Time = TimeTo.Time;
                ErrorInterval.Text = "Неверный интервал";
            }
        }

        private void MyBook_OnClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new MyBooks(CurrentUser));
        }
    }
}
