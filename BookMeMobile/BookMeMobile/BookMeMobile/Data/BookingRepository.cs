using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Interface;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace BookMeMobile.Data
{
    public class ReservationRepository
    {
        private HttpService httpService;

        public ReservationRepository()
        {
            this.httpService = new HttpService();
        }

        public async Task<BaseOperationResult> RemoveReservation(int id)
        {
            return (await this.httpService.Delete(string.Format(RestURl.BookURl + "{0}", id)));
        }

        public async Task<BaseOperationResult> AddReservation(RoomFilterParameters reservation, int idRoom)
        {
            Reservation model = new Reservation()
            {
                From = reservation.From,
                To = reservation.To,
                Room = new Room()
                {
                    Id = idRoom
                },
                IsRecursive = false,
                ResourceId = idRoom,
                Duration = reservation.To - reservation.From
            };
            return await this.httpService.Post(RestURl.BookURl, model);
        }

        public async Task<BaseOperationResult<IEnumerable<Reservation>>> GetUserReservations()
        {
            return await this.httpService.Get<IEnumerable<Reservation>>(RestURl.GetUserReservation);
        }
    }
}