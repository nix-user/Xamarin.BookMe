using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Entity;
using Java.Nio.Channels;
using Xamarin.Forms;

namespace App2.Pages
{
    public partial class MyBooks : ContentPage
    {
        public List<MyBookViewResult> ResultRoom { get; set; }
        public User CurrentUser { get; set; }
        public MyBooks(User user)
        {
            InitializeComponent();
            CurrentUser = user;
            ResultRoom = Serch();
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
        private List<MyBookViewResult> Serch()
        {
            List<MyBookViewResult> result=new List<MyBookViewResult>();
            foreach (Room room in ListRoomPage.rooms)
            {
                foreach (Booking booking in room.Bookings)
                {
                    if (booking.WhoBook==CurrentUser)
                    {
                        result.Add(new MyBookViewResult()
                        {
                            Room = room.Number,
                            Date = booking.Date,
                            From = booking.From,
                            To = booking.To,
                            IsHasPolynom = room.IsHasPolynom,
                            IsBig = room.IsBig
                        });
                    }
                }
            }
            return result;
        }
    }
}
