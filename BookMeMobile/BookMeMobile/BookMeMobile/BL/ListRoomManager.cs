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
            this.bookings = new BookingRepository();
            this.rooms = new RoomRepository();
            this.currentUser = user;
        }

        public List<MyBookViewResult> AddUserBookInRange(Booking booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (
                Room room in
                    this.rooms.GetAll()
                        .Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
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
            foreach (
                Room room in
                    this.rooms.GetAll()
                        .Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    bool endInRange = book.From <= booking.From && book.To >= booking.From && booking.To >= book.To;
                    bool startInRange = book.From <= booking.To && book.To >= booking.To && booking.From <= book.From;
                    if ((endInRange | startInRange) & book.WhoBook == booking.WhoBook && book.Date == booking.Date)
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

            return this.Sort(result);
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
                List<Booking> searchBook = new List<Booking>();
                bool hasRange = true;
                if (room.IsRecursive)
                {
                    searchBook.AddRange(item.Bookings.Where(x => x.Date >= room.Date));
                }
                else
                {
                    searchBook.AddRange(item.Bookings.Where(x => x.Date.Date == room.Date.Date));
                }

                foreach (Booking book in searchBook)
                {
                    if (book.From >= room.To || book.To <= room.From)
                    {
                        hasRange = true;
                    }
                    else
                    {
                        hasRange = false;
                        break;
                    }
                }

                if (hasRange)
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

            return this.Sort(result);
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

        public void AddBookInWeek(int idRoom)
        {
            for (int i = 1; i <= 7; i++)
            {
                Booking currentBook = new Booking()
                {
                    IsRecursive = true,
                    Room = this.currentBooking.Room,
                    Id = this.currentBooking.Id,
                    Date = this.currentBooking.Date,
                    From = this.currentBooking.From,
                    To = this.currentBooking.To,
                    WhoBook = this.currentBooking.WhoBook
                };
                this.currentBooking.Id = counter++;
                this.rooms.GetAll().FirstOrDefault(x => x.Id == idRoom).Bookings.Add(currentBook);
                this.bookings.AddBooking(currentBook);
                this.currentBooking.Date = DateTime.Now.AddDays(i);
            }
        }

        public void AddBook(int idRoom)
        {
            this.currentBooking.Id = counter++;
            this.rooms.GetAll().FirstOrDefault(x => x.Id == idRoom).Bookings.Add(this.currentBooking);
            this.bookings.AddBooking(this.currentBooking);
        }

        public void AddBook(Booking book)
        {
            book.Id = counter++;
            this.rooms.GetAll().FirstOrDefault(x => x.Id == book.Room.Id).Bookings.Add(book);
            this.bookings.AddBooking(book);
        }

        public void DeleteBook(int idBooking)
        {
            Booking deleteBook = this.bookings.GetBook(idBooking);
            this.bookings.RemoveBook(deleteBook.Id);
            foreach (Room room in this.rooms.GetAll())
            {
                room.Bookings.Remove(deleteBook);
            }
        }

        public List<MyBookViewResult> GetUserBookings()
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();

            foreach (Booking booking in this.bookings.GetAll().Where(x => x.IsRecursive == false))
            {
                if (booking.WhoBook == this.currentUser)
                {
                    result.Add(new MyBookViewResult()
                    {
                        Room = booking.Room.Number,
                        Date = booking.Date,
                        From = booking.From,
                        To = booking.To,
                        IsHasPolykom = booking.Room.IsHasPolykom,
                        IsBig = booking.Room.IsBig,
                        IsBook = false,
                        Id = booking.Id
                    });
                }
            }

            return result;
        }

        public List<MyBookViewResult> GetUserBookingsRecursive()
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            List<Booking> allBooksOne = new List<Booking>();

            foreach (
                Booking booking in this.bookings.GetAll().Where(x => x.WhoBook == this.currentUser && x.IsRecursive))
            {
                allBooksOne.Add(booking);
            }

            result = allBooksOne.GroupBy(x => new { x.From, x.To, x.Room }).Select(x => new MyBookViewResult()
            {
                Id = x.First().Id,
                From = x.Key.From,
                To = x.Key.To,
                Room = x.Key.Room.Number,
                Date = x.First().Date,
                IsRecursive = true,
                IsBig = x.First().Room.IsBig,
                IsHasPolykom = x.FirstOrDefault().Room.IsHasPolykom
            }).ToList();

            return result;
        }

        public void DeleteBookRecursive(int idBook)
        {
            Booking book = this.bookings.GetAll().FirstOrDefault(x => x.Id == idBook);
            foreach (Booking booking in this.rooms.GetAll().FirstOrDefault(x => x.Id == book.Room.Id).Bookings.Where(y => y.From == book.From && y.To == book.To))
            {
                this.bookings.RemoveBook(booking.Id);
                this.rooms.GetAll().FirstOrDefault(x => x.Number == booking.Room.Number).Bookings.Remove(booking);
            }
        }

        public List<MyBookViewResult> Sort(List<MyBookViewResult> list)
        {
            int userFloor = this.GetFloorInNumber(this.currentUser.MyRoom);
            list.Sort((view1, view2) =>
            {
                if (Math.Abs(GetFloorInNumber(view1.Room.ToString()) - userFloor) >
                    Math.Abs(GetFloorInNumber(view2.Room.ToString()) - userFloor))
                {
                    return 1;
                }
                else
                {
                    if (Math.Abs(GetFloorInNumber(view1.Room.ToString()) - userFloor) <
                        Math.Abs(GetFloorInNumber(view2.Room.ToString()) - userFloor))
                    {
                        return -1;
                    }
                    else
                    {
                        return 0;
                    }
                }
            });
            if (list.FindIndex(x => x.Room == this.currentUser.FavoriteRoom) > 0)
            {
                MyBookViewResult first = list[list.FindIndex(x => x.Room == this.currentUser.FavoriteRoom)];
                list.Remove(first);
                list.Insert(0, first);
            }

            return list;
        }

        public int GetFloorInNumber(string s)
        {
            string stringFloor = new string(s.ToCharArray().Where(ch => !char.IsLetter(ch)).ToArray());
            int floor;
            if (stringFloor.Length < 4)
            {
                return floor = int.Parse(stringFloor[0].ToString());
            }
            else
            {
                return floor = int.Parse(stringFloor[0].ToString() + stringFloor[1]);
            }
        }

        public Booking AttemptBook(string result, User user)
        {
            bool roomBook = false;
            Room currentRoom = this.rooms.GetAll().FirstOrDefault(x => x.Number == result);
            foreach (Booking book in currentRoom.Bookings.Where(x => x.Date.Date == DateTime.Now.Date))
            {
                if (book.From >= DateTime.Now.TimeOfDay || book.To <= DateTime.Now.TimeOfDay)
                {
                    roomBook = false;
                }
                else
                {
                    if (book.WhoBook == this.currentUser)
                    {
                        roomBook = false;
                    }
                    else
                    {
                        roomBook = true;
                        return null;
                    }
                }
            }

            return new Booking()
            {
                Date = DateTime.Now,
                Room = currentRoom,
                From = DateTime.Now.Subtract(DateTime.Now.Date),
                Id = DateTime.Now.Millisecond + new Random().Next(1000),
                To = DateTime.Now.AddHours(1).Subtract(DateTime.Now.Date),
                WhoBook = this.currentUser
            };
        }
    }
}