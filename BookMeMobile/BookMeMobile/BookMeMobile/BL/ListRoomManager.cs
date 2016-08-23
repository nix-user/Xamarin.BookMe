using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.BL
{
    public class ListRoomManager
    {
        private static int counter = 0;

        private RoomRepository rooms;
        private BookingRepository bookings;

        private Booking currentBooking;
        private User currentUser;

        public ListRoomManager(Booking book, User currentUser)
        {
            this.bookings = new BookingRepository();
            this.rooms = new RoomRepository();
            this.currentBooking = book;
            this.currentUser = currentUser;
        }

        public ListRoomManager(User user)
        {
            this.currentUser = user;
        }

        public List<MyBookViewResult> AddUserBookInRange(Booking booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in this.rooms.GetAll().Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings.Where(x => x.WhoBook == booking.WhoBook && x.Date == booking.Date))
                {
                    if (book.From < booking.From && book.To > booking.To)
                    {
                        result.Add(new MyBookViewResult()
                        {
                            From = book.From,
                            To = book.To,
                            Room = book.Room.Number,
                            InRange = true,
                            IsBook = true
                        });
                    }
                }
            }

            return result;
        }

        public List<MyBookViewResult> AddUserBookPartRange(Booking booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in this.rooms.GetAll().Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    bool endInRange = book.From <= booking.From && book.To >= booking.From && booking.To >= book.To;
                    bool startInRange = book.From <= booking.To && book.To >= booking.To && booking.From <= book.From;
                    if ((endInRange | startInRange) && book.WhoBook == booking.WhoBook && book.Date == booking.Date)
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

        private List<Room> ConditionTrue(Booking room)
        {
            if (room.Room.IsBig && room.Room.IsHasPolykom)
            {
                return this.rooms.GetAll().Where(x => x.IsBig && x.IsHasPolykom).ToList();
            }

            if (!room.Room.IsBig && room.Room.IsHasPolykom)
            {
                return this.rooms.GetAll().Where(x => (x.IsBig || !x.IsBig) && x.IsHasPolykom).ToList();
            }

            if (room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                return this.rooms.GetAll().Where(x => (x.IsHasPolykom || !x.IsHasPolykom) && x.IsBig).ToList();
            }

            if (!room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                return this.rooms.GetAll().Where(x => true).ToList();
            }

            return null;
        }

        public List<MyBookViewResult> Search(Booking room)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            List<Room> srchList = this.ConditionTrue(room);

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
                }
            }

            return result;
        }

        public string Booking(int idRoom)
        {
            this.currentBooking.Room = this.rooms.GetRoom(idRoom);
            Room currentRoom = this.rooms.GetRoom(idRoom);
            return string.Format(
                " Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n Большая:{4} Поликом:{5}",
                this.currentBooking.Date.ToString("d"),
                this.currentBooking.From.ToString(@"hh\:mm"),
                this.currentBooking.To.ToString(@"hh\:mm"),
                currentRoom.Number,
                string.Format("{0:Да;0;Нет}", currentRoom.IsBig.GetHashCode()),
                string.Format("{0:Да;0;Нет}", currentRoom.IsHasPolykom.GetHashCode()));
        }

        public void AddBook(int idRoom)
        {
            this.currentBooking.Id = counter++;
            this.rooms.GetAll().FirstOrDefault(x => x.Id == idRoom).Bookings.Add(this.currentBooking);
            this.bookings.AddBooking(this.currentBooking);
        }

        public void DeleteBook(int idBooking)
        {
            Booking deleteBook = this.bookings.GetBook(idBooking);
            this.bookings.ReoveBook(deleteBook.Id);
            foreach (Room room in this.rooms.GetAll())
            {
                room.Bookings.Remove(deleteBook);
            }
        }

        public List<MyBookViewResult> GetUserBookings()
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in this.rooms.GetAll())
            {
                foreach (Booking booking in room.Bookings)
                {
                    if (booking.WhoBook == this.currentUser)
                    {
                        result.Add(new MyBookViewResult()
                        {
                            Room = room.Number,
                            Date = booking.Date,
                            From = booking.From,
                            To = booking.To,
                            IsHasPolykom = room.IsHasPolykom,
                            IsBig = room.IsBig,
                            IsBook = false,
                            Id = booking.Id
                        });
                    }
                }
            }

            return result;
        }
    }
}