using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.BL
{
    public class RoomRepository
    {
        private static List<Room> rooms = new List<Room>()
        {
              new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "304D",
                Id = 6,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "505E",
                Id = 5,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = false,
                Number = "403D",
                Id = 1,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = "202K",
                Id = 2,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = true,
                IsHasPolykom = false,
                Number = "323P",
                Id = 3,
                Bookings = new List<Booking>()
            },
            new Room()
            {
                IsBig = false,
                IsHasPolykom = true,
                Number = "678T",
                Id = 4,
                Bookings = new List<Booking>()
            }
        };

        public IEnumerable<Room> GetAll()
        {
            return rooms.Where(x => true);
        }

        public Room GetRoom(int id)
        {
            return rooms.FirstOrDefault(x => x.Id == id);
        }

        public void AddRoom(Room room)
        {
            rooms.Add(room);
        }
    }
}