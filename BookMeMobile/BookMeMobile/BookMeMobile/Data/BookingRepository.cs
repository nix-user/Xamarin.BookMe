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

        public async Task<StatusCode> RemoveReservation(int id)
        {
            return (await this.httpService.Delete(RestURl.BookURl + "{0}", id)).Status;
        }

        public async Task<OperationResult<object>> AddReservation(int idRoom, ReservationModel reservation)
        {
            reservation.Room.Id = idRoom;
            return await this.httpService.Post<ReservationModel, object>(RestURl.BookURl, reservation);
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetUserReservations()
        {
            return await this.httpService.Get<IEnumerable<ReservationModel>>(RestURl.GetUserReservation);
        }
    }
}