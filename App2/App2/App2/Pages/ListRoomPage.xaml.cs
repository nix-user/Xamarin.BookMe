using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App2.Entity;
using Xamarin.Forms;

namespace App2.Pages
{
    public partial class ListRoomPage : ContentPage
    {

        public static List<Room> rooms = new List<Room>()
        {
              new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 6,
                Id = 6,
                Bookings =
                    new List<Booking>()
                    {
                    }
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 5,
                Id = 5,
                Bookings =
                    new List<Booking>()
                    {
                    }
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 1,
                Id = 1,
                Bookings =
                    new List<Booking>()
                    {
                       
                    }
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 2,
                Id = 2,
                Bookings =
                    new List<Booking>()
                    {
                      
                    }
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 3,
                Id = 3,
                Bookings =
                    new List<Booking>()
                    {
                      
                    }
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 4,
                Id = 4,
                Bookings =
                    new List<Booking>()
                    {
                       
                    }
            }
        };

        public List<Room> ResultRoom { get; set; }
        public Booking CurreBooking;
        public ListRoomPage(Booking book)
        {
            InitializeComponent();
            CurreBooking = book;
            ResultRoom = Search(book);
            //AddUserBookInRange(book);
            //AddUserBookPartRange(book);
            if (!ResultRoom.Any())
            {
                listRoom.Header = ListRoomIsEmpty();
            }
            listRoom.BindingContext = ResultRoom;
        }

        //private void AddUserBookInRange(Booking booking)
        //{
        //    List<MyBookViewResult> result = new List<MyBookViewResult>();
        //    foreach (Room room in rooms)
        //    {
        //        foreach (var book in room.Bookings)
        //        {
        //            if (book.From < booking.From && book.To > booking.To && book.WhoBook == booking.WhoBook)
        //            {
        //                result.Add(new MyBookViewResult()
        //                {
        //                    Date = book.Date,
        //                    From = book.From,
        //                    To = book.To,
        //                    Room = book.Room.Number,
        //                    IsHasPolykom = room.IsHasPolykom,
        //                    IsBig = room.IsBig
        //                });
        //            }
        //        }
        //    }
        //    if (result.Any())
        //    {
        //        listUserRoomInRange.ItemsSource = result;
        //    }
        //    else
        //    {
        //        listUserRoomInRange.IsVisible = false;
        //    }
        //}

        //public void AddUserBookPartRange(Booking booking)
        //{
        //    List<MyBookViewResult> result = new List<MyBookViewResult>();
        //    foreach (Room room in rooms)
        //    {
        //        foreach (var book in room.Bookings)
        //        {
        //            if (((book.From < booking.From && book.To > booking.From&&booking.To>book.To) | (book.From < booking.To && book.To > booking.To&&booking.From<book.From)) && book.WhoBook == booking.WhoBook)
        //            {
        //                result.Add(new MyBookViewResult()
        //                {
        //                    Date = book.Date,
        //                    From = book.From,
        //                    To = book.To,
        //                    Room = book.Room.Number,
        //                    IsHasPolykom = room.IsHasPolykom,
        //                    IsBig = room.IsBig
        //                });
        //            }
        //        }
        //    }
        //    if (result.Any())
        //    {
        //        listUserRoomPartInRange.ItemsSource = result;
        //    }
        //    else
        //    {
        //        listUserRoomPartInRange.IsVisible = false;
        //    }
        //}

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

        public List<Room> Search(Booking room)
        {
            List<Room> result = new List<Room>();
            foreach (var item in rooms.Where(x => x.IsBig == room.Room.IsBig && x.IsHasPolykom == room.Room.IsHasPolykom))
            {
                bool b = true;
                foreach (Booking book in item.Bookings.Where(x => x.Date == room.Date))
                {
                    if (book.From >= room.To || book.To <= room.From)
                    {
                        b = true;
                    }
                    else
                    {
                        b = false;
                        break;
                    }
                }

                if (b)
                {
                    result.Add(item);
                }

            }
            return result;
        }

        private async void BtnBooking_OnClicked(object sender, EventArgs e)
        {
            string idButton = ((Button)sender).ClassId;
            int idRoom = int.Parse(idButton);
            CurreBooking.Room = rooms.FirstOrDefault(x => x.Id == idRoom);
            rooms.FirstOrDefault(x => x.Id == idRoom).Bookings.Add(CurreBooking);
            int numberRoom = rooms.FirstOrDefault(x => x.Id == idRoom).Number;
            await DisplayAlert("Забукали!!!!", string.Format(" Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n ", CurreBooking.Date.ToString("d"), CurreBooking.From.ToString(@"hh\:mm"), CurreBooking.To.ToString(@"hh\:mm"), numberRoom.ToString()), "OK");
            await Navigation.PopAsync();
        }
    }
}
