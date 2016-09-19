using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Android.App;
using BookMeMobile.Entity;
using BookMeMobile.Model;
using BookMeMobile.OperationResults;
using Newtonsoft.Json;

namespace BookMeMobile.Data
{
    public class RoomRepository : BaseRepository
    {
        public async Task<OperationResult<IEnumerable<Room>>> GetAllRoom()
        {
            return await this.HttpService.Get<IEnumerable<Room>>(RestURl.RoomURl);
        }

        public async Task<OperationResult<Room>> GetRoom(int id)
        {
            return await this.HttpService.Get<Room>(RestURl.RoomURl + id);
        }

        public async Task<OperationResult<IEnumerable<Room>>> GetEmptyRoom(RoomFilterParameters filter)
        {
            filter.From = filter.From.AddHours(-3);
            filter.To = filter.From.AddHours(-3);
            var root = string.Format(RestURl.GetEmptyRoom, filter.From.ToString(new CultureInfo("en-US")), filter.To.ToString(new CultureInfo("en-US")), filter.HasPolycom, filter.IsLarge);
            return await this.HttpService.Get<IEnumerable<Room>>(root);
        }

        public async Task<OperationResult<IEnumerable<ReservationModel>>> GetCurrentRoomReservation(RoomReservationsRequestModel reservationsModel)
        {
            reservationsModel.From = reservationsModel.From.AddHours(-3);
            reservationsModel.To = reservationsModel.From.AddHours(-3);
            var root =
                string.Format(
                RestURl.GetCurrentRoomReservation, reservationsModel.From.ToString(new CultureInfo("en-US")), reservationsModel.To.ToString(new CultureInfo("en-US")), reservationsModel.RoomId);
            return await this.HttpService.Get<IEnumerable<ReservationModel>>(root);
        }
    }
}