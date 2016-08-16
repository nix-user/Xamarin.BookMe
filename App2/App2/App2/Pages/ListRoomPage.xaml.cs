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
                IsBig = true,
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
                IsBig = true,
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
                IsHasPolykom = true,
                Number = 3,
                Id = 3,
                Bookings =
                    new List<Booking>()
                    {

                    }
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = true,
                Number = 4,
                Id = 4,
                Bookings =
                    new List<Booking>()
                    {

                    }
            }
        };

        public List<MyBookViewResult> ResultRoom { get; set; }
        public Booking CurreBooking;
        public ListRoomPage(Booking book)
        {
            InitializeComponent();
            CurreBooking = book;
            ResultRoom=AddUserBookInRange(book);
            ResultRoom.AddRange(AddUserBookPartRange(book));
            ResultRoom.AddRange(Search(book));
            if (!ResultRoom.Any())
            {
                 isRoom.IsVisible = true;
            }
            listUserRoomInRange.BindingContext = ResultRoom;
        }

        private List<MyBookViewResult> AddUserBookInRange(Booking booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in rooms.Where(x=>x.IsBig==booking.Room.IsBig&&x.IsHasPolykom==booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    if (book.From <= booking.From && book.To >= booking.To && book.WhoBook == booking.WhoBook)
                    {
                        result.Add(new MyBookViewResult()
                        {
                            From = book.From,
                            To = book.To,
                            Room = book.Room.Number,
                            InRange = true,
                            IsBook=true
                        });
                    }
                }
            }
            return result;
        }

        public List<MyBookViewResult> AddUserBookPartRange(Booking booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in rooms.Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    if (((book.From < booking.From && book.To > booking.From && booking.To > book.To) | (book.From < booking.To && book.To > booking.To && booking.From < book.From)) && book.WhoBook == booking.WhoBook)
                    {
                        result.Add(new MyBookViewResult()
                        {
                            Date = book.Date,
                            From = book.From,
                            To = book.To,
                            Room = book.Room.Number,
                            IsHasPolykom = room.IsHasPolykom,
                            IsBig = room.IsBig,
                            InRange = false,
                            IsBook = true
                        });
                    }
                }
            }
            return result;
        }

        public List<MyBookViewResult> Search(Booking room)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            List<Room> srchList = new List<Room>();
            if (room.Room.IsBig&&room.Room.IsHasPolykom)
            {
                srchList = rooms.Where(x => x.IsBig&&x.IsHasPolykom).ToList();
            }
            if(!room.Room.IsBig && room.Room.IsHasPolykom)
            {
                srchList = rooms.Where(x => (x.IsBig || !x.IsBig)&&x.IsHasPolykom).ToList();
            }
            if (room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                srchList = rooms.Where(x =>(x.IsHasPolykom || !x.IsHasPolykom)&&x.IsBig).ToList();
            }
            if (!room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                srchList = rooms.Where(x =>true).ToList();
            }


            foreach (var item in srchList)
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
                    result.Add(new MyBookViewResult()
                    {
                        IsHasPolykom = item.IsHasPolykom,
                        IsBig = item.IsBig,
                        Room = item.Number,
                        Id = item.Id,
                        IsBook = false,
                        InRange = null
                        
                    });
                };
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
