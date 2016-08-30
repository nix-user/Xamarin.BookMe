using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;

namespace BookMeMobile.BL
{
    public class BookingRepository
    {
        private static List<Booking> bookings = new List<Booking>();

        public IEnumerable<Booking> GetAll()
        {
            return bookings.Where(x => true);
        }

        public Booking GetBook(int id)
        {
            return bookings.FirstOrDefault(x => x.Id == id);
        }

        public void ReoveBook(int id)
        {
            Booking removingBook = this.GetBook(id);
            bookings.Remove(removingBook);
        }

        public void AddBooking(Booking book)
        {
            bookings.Add(book);
        }
    }
}