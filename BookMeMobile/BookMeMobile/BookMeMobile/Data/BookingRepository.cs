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

        public async Task<OperationResult> RemoveReservation(int id)
        {
            return (await this.httpService.Delete(string.Format(RestURl.BookURl + "{0}", id)));
        }

        public async Task<OperationResult> AddReservation(int idRoom, ReservationModel reservation)
        {
            reservation.Room.Id = idRoom;
            return await this.httpService.Delete(string.Format(RestURl.BookURl + reservation.Id));
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetUserReservations()
        {
            return await this.httpService.Get<IEnumerable<ReservationModel>>(RestURl.GetUserReservation);
        }
    }
}