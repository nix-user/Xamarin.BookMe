using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    public class ReservationRepository : BaseRepository
    {
        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetAll()
        {
            return await this.HttpService.Get<IEnumerable<ReservationModel>>(RestURl.BookURl);
        }

        public async Task<OperationResult<ReservationModel>> GetReservation(int id)
        {
            return await this.HttpService.Get<ReservationModel>(RestURl.BookURl + id);
        }

        public async Task<OperationResult> RemoveReservation(int id)
        {
            return await this.HttpService.Delete(RestURl.BookURl + id);
        }

        public async Task<OperationResult> AddReservation(int idRoom, ReservationModel reservation)
        {
            return await this.HttpService.Post<ReservationModel>(RestURl.BookURl, reservation);
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetUserReservations(string login)
        {
            return await this.HttpService.Get<IEnumerable<ReservationModel>>(string.Format(RestURl.GetUserReservation, login));
        }
    }
}