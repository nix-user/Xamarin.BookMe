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
        private BookingRepository reservation;

        private ReservationModel currentBooking;
        private User currentUser;

        public ListRoomManager(ReservationModel book, User currentUser)
        {
            this.reservation = new BookingRepository();
            this.rooms = new RoomRepository();
            this.currentBooking = book;
            this.currentUser = currentUser;
        }

        public ListRoomManager(User user)
        {
            this.reservation = new BookingRepository();
            this.rooms = new RoomRepository();
            this.currentUser = user;
        }

        public List<MyBookViewResult> AddUserBookInRange(ReservationModel booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (
                Room room in
                    this.rooms.GetAllRoom().Result
                        .Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings.Where(x => x.Author.Id == booking.Author.Id && x.Date == booking.Date))
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

        public List<MyBookViewResult> AddUserBookPartRange(ReservationModel booking)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (
                Room room in
                    this.rooms.GetAllRoom().Result
                        .Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    bool endInRange = book.From <= booking.From && book.To >= booking.From && booking.To >= book.To;
                    bool startInRange = book.From <= booking.To && book.To >= booking.To && booking.From <= book.From;
                    if ((endInRange | startInRange) & book.Author.Id == booking.Author.Id && book.Date == booking.Date)
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

        private List<Room> ConditionTrue(ReservationModel reservation)
        {
            if (reservation.Room.IsBig && reservation.Room.IsHasPolykom)
            {
                return this.rooms.GetAllRoom().Result.Where(x => x.IsBig && x.IsHasPolykom).ToList();
            }

            if (!reservation.Room.IsBig && reservation.Room.IsHasPolykom)
            {
                return this.rooms.GetAllRoom().Result.Where(x => (x.IsBig || !x.IsBig) && x.IsHasPolykom).ToList();
            }

            if (reservation.Room.IsBig && !reservation.Room.IsHasPolykom)
            {
                return this.rooms.GetAllRoom().Result.Where(x => (x.IsHasPolykom || !x.IsHasPolykom) && x.IsBig).ToList();
            }

            if (!reservation.Room.IsBig && !reservation.Room.IsHasPolykom)
            {
                return this.rooms.GetAllRoom().Result.Where(x => true).ToList();
            }

            return null;
        }

        public List<MyBookViewResult> Search(ReservationModel reservation)
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            List<Room> srchList = this.ConditionTrue(reservation);

            foreach (var item in srchList)
            {
                List<ReservationModel> searchBook = new List<ReservationModel>();
                bool hasRange = true;
                if (reservation.IsRecursive)
                {
                    searchBook.AddRange(item.Bookings.Where(x => x.Date >= reservation.Date));
                }
                else
                {
                    searchBook.AddRange(item.Bookings.Where(x => x.Date.Date == reservation.Date.Date));
                }

                foreach (ReservationModel book in searchBook)
                {
                    if (book.From >= reservation.To || book.To <= reservation.From)
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

        public async Task<string> Booking(int idRoom)
        {
            this.currentBooking.Room = await this.rooms.GetRoom(idRoom);
            Room currentRoom = this.rooms.GetRoom(idRoom).Result;
            return string.Format(
                " Комната: {3}\n Дата: {0}\n Время: {1} - {2}\n Большая:{4} Поликом:{5}",
                this.currentBooking.Date.ToString("d"),
                this.currentBooking.From.ToString(@"hh\:mm"),
                this.currentBooking.To.ToString(@"hh\:mm"),
                currentRoom.Number,
                string.Format("{0:Да;0;Нет}", currentRoom.IsBig.GetHashCode()),
                string.Format("{0:Да;0;Нет}", currentRoom.IsHasPolykom.GetHashCode()));
        }

        public async void AddBookInWeek(int idRoom)
        {
            for (int i = 1; i <= 7; i++)
            {
                ReservationModel currentBook = new ReservationModel()
                {
                    IsRecursive = true,
                    Room = this.currentBooking.Room,
                    Id = this.currentBooking.Id,
                    Date = this.currentBooking.Date,
                    From = this.currentBooking.From,
                    To = this.currentBooking.To,
                    Author = this.currentBooking.Author
                };
                this.currentBooking.Id = counter++;
                this.rooms.GetAllRoom().Result.FirstOrDefault(x => x.Id == idRoom).Bookings.Add(currentBook);
                await this.reservation.AddBooking(currentBook);
                this.currentBooking.Date = DateTime.Now.AddDays(i);
            }
        }

        public async void AddBook(int idRoom)
        {
            this.currentBooking.Id = counter++;
            await this.reservation.AddBooking(this.currentBooking);
        }

        public async void AddBook(ReservationModel book)
        {
            book.Id = counter++;
            await this.reservation.AddBooking(book);
        }

        public async Task<bool> DeleteBook(int idBooking)
        {
            ReservationModel deleteBook = this.reservation.GetBook(idBooking).Result;
            return await this.reservation.RemoveBook(deleteBook.Id);
        }

        public List<MyBookViewResult> GetUserBookings()
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();

            foreach (ReservationModel booking in this.reservation.GetAll().Result.Where(x => x.IsRecursive == false))
            {
                if (booking.Author.Id == this.currentUser.Id)
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
            List<ReservationModel> allBooksOne = this.reservation.GetAll().Result.Where(x => x.Author.Id == this.currentUser.Id && x.IsRecursive).ToList();

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

        public async Task<bool> DeleteBookRecursive(int idBook)
        {
            return await this.reservation.RemoveBook(idBook);
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

        public ReservationModel AttemptBook(string result, User user)
        {
            bool roomBook = false;
            Room currentRoom = this.rooms.GetAllRoom().Result.FirstOrDefault(x => x.Number == result);
            foreach (ReservationModel book in currentRoom.Bookings.Where(x => x.Date.Date == DateTime.Now.Date))
            {
                if (book.From >= DateTime.Now.TimeOfDay || book.To <= DateTime.Now.TimeOfDay)
                {
                    roomBook = false;
                }
                else
                {
                    if (book.Author.Id == this.currentUser.Id)
                    {
                        return new ReservationModel()
                        {
                            Date = DateTime.Now,
                            Room = currentRoom,
                            From = book.To,
                            Id = DateTime.Now.Millisecond + new Random().Next(1000),
                            To = book.To.Add(new TimeSpan(1, 0, 0)),
                            Author = this.currentUser
                        };
                    }
                    else
                    {
                        roomBook = true;
                        return null;
                    }
                }
            }

            return new ReservationModel()
            {
                Date = DateTime.Now,
                Room = currentRoom,
                From = DateTime.Now.Subtract(DateTime.Now.Date),
                Id = DateTime.Now.Millisecond + new Random().Next(1000),
                To = DateTime.Now.AddHours(1).Subtract(DateTime.Now.Date),
                Author = this.currentUser
            };
        }
    }
}