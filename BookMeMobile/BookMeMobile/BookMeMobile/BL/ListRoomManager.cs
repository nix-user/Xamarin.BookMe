﻿using System;
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

        private readonly List<Booking> bookings = new List<Booking>();
        private readonly List<Room> rooms = new List<Room>()
        {
              new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 6,
                Id = 6,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 5,
                Id = 5,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = 1,
                Id = 1,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = 2,
                Id = 2,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = 3,
                Id = 3,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = true,
                Number = 4,
                Id = 4,
                Bookings = new List<Booking>()
            }
        };

        private Booking currentBooking;
        private User currentUser;
        
        public ListRoomManager(Booking book, User currentUser)
        {
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
            foreach (Room room in this.rooms.Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
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
            foreach (Room room in this.rooms.Where(x => x.IsBig == booking.Room.IsBig && x.IsHasPolykom == booking.Room.IsHasPolykom))
            {
                foreach (var book in room.Bookings)
                {
                    if (((book.From <= booking.From && book.To >= booking.From && booking.To >= book.To) | (book.From <= booking.To && book.To >= booking.To && booking.From <= book.From)) && book.WhoBook == booking.WhoBook && book.Date == booking.Date)
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
                return this.rooms.Where(x => x.IsBig && x.IsHasPolykom).ToList();
            }

            if (!room.Room.IsBig && room.Room.IsHasPolykom)
            {
                return this.rooms.Where(x => (x.IsBig || !x.IsBig) && x.IsHasPolykom).ToList();
            }

            if (room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                return this.rooms.Where(x => (x.IsHasPolykom || !x.IsHasPolykom) && x.IsBig).ToList();
            }

            if (!room.Room.IsBig && !room.Room.IsHasPolykom)
            {
                return this.rooms.Where(x => true).ToList();
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
            this.currentBooking.Room = this.rooms.FirstOrDefault(x => x.Id == idRoom);
            Room currentRoom = this.rooms.FirstOrDefault(x => x.Id == idRoom);
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
            this.rooms.FirstOrDefault(x => x.Id == idRoom).Bookings.Add(this.currentBooking);
            this.bookings.Add(this.currentBooking);
        }

        public void DeleteBook(int idBooking)
        {
            Booking deleteBook = this.bookings.FirstOrDefault(x => x.Id == idBooking);
            this.bookings.Remove(deleteBook);
            foreach (Room room in this.rooms)
            {
                room.Bookings.Remove(deleteBook);
            }
        }

        public List<MyBookViewResult> GetUserBookings()
        {
            List<MyBookViewResult> result = new List<MyBookViewResult>();
            foreach (Room room in this.rooms)
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